using AutoMapper;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.OfferRepositories;
using Buyersoft.Domain.Repositories.OrderRepositories;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class OrderService : IOrderService
{
    private readonly IAddOrderRepository _addOrderRepository;
    private readonly IUpdateOrderRepository _updateOrderRepository;
    private readonly IDeleteOrderRepository _deleteOrderRepository;
    private readonly IQueryOrderRepository _queryOrderRepository;
    private readonly IQueryRequestRepository _queryRequestRepository;
    private readonly IQueryOfferRepository _queryOfferRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly UserManager<User> _userManager;

    public OrderService(IAddOrderRepository addOrderRepository,
        IUpdateOrderRepository updateOrderRepository,
        IDeleteOrderRepository deleteOrderRepository,
        IQueryOrderRepository queryOrderRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IQueryRequestRepository queryRequestRepository,
        IQueryOfferRepository queryOfferRepository,
        INotificationService notificationService,
        UserManager<User> userManager)
    {
        _addOrderRepository = addOrderRepository;
        _updateOrderRepository = updateOrderRepository;
        _deleteOrderRepository = deleteOrderRepository;
        _queryOrderRepository = queryOrderRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _queryRequestRepository = queryRequestRepository;
        _queryOfferRepository = queryOfferRepository;
        _notificationService = notificationService;
        _userManager = userManager;
    }


    public async Task<PaginatedList<OrderPaginationDto>> GetAllAsync(int companyId, int supplierId, OrderPreparationFilterDto filter, PageRequest pagination)
    {

        var query = _queryOrderRepository.GetList(x => x.OrderPreparation.CompanyId == companyId || x.OrderPreparation.Offer.CompanyId == companyId)
            .Where(x => x.Status == filter.status)
            .Include(x => x.OrderPreparation)
            .Include(x => x.OrderItems)
            .Select(x => new OrderPaginationDto()
            {
                Id = x.Id,
                OrderPreparation = new OrderPreparationListDto()
                {
                    RequestId = x.OrderPreparation.RequestId,
                    OfferId = x.OrderPreparation.OfferId,
                    Supplier = x.OrderPreparation.Offer.Company.Name,
                    MainCategory = x.OrderPreparation.MainCategory,
                    SubCategory = x.OrderPreparation.SubCategory,
                    RequestGroup = x.OrderPreparation.RequestGroup,
                    RequestCode = x.OrderPreparation.RequestCode,
                    CurrencyCode = x.OrderPreparation.Request.Currency.Code,
                    ReferenceCode = x.OrderPreparation.ReferenceCode,
                    Unit = x.OrderPreparation.Request.Category.Unit,
                },

                TotalPrice = x.TotalPrice,
                OrderItems = x.OrderItems.Select(oi => new OrderItemListDto(
                    oi.Id,
                    oi.OfferDetailId,
                    oi.ProductDefinition,
                    oi.UnitPrice,
                    oi.TotalPrice,
                    oi.Quantity
                )).ToList(),
                OrderCode = x.OrderCode,
                OrderDate = x.OrderDate,
                Status = x.Status
            });

        if (filter.status == OrderStatusEnum.OrderPending)
        {
            query = query.Where(x => x.Status == OrderStatusEnum.OrderPending || x.Status == OrderStatusEnum.InProduction || x.Status == OrderStatusEnum.Shipped);
        }
        else
        {
            query = query.Where(x => x.Status == filter.status);
        }

        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
        .ToListAsync();


        return new PaginatedList<OrderPaginationDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task SetNonconformityAsync(SetNonconformityDto Model)
    {
        var order = await _queryOrderRepository.GetFirstAsync(x => x.Id == Model.Id)
            .Include(x => x.OrderPreparation)
                .ThenInclude(x => x.Company)
                        .Include(x => x.OrderPreparation)
                .ThenInclude(x => x.Offer)
                    .ThenInclude(x => x.Company)
            .FirstAsync();

        order.NonconformityDetail = Model.Detail;
        order.NonconformityReason = Model.Status;
        order.Status = OrderStatusEnum.NonconformityReported;

        _updateOrderRepository.Update(order);

        var users = await _userManager.Users.Where(x => x.CompanyId == order.OrderPreparation.Offer.CompanyId && x.RoleId == 3).ToListAsync();

        foreach (var user in users)
        {
            string message = $"{order.OrderPreparation.Company.Name} Firması, {order.OrderCode} Referans Numaralı Siparişle İlgili Uygunsuzluk Bildirimi Gönderdi";

            var notificationDto = new NotificationDto(0, user.Id, message, false);

            await _notificationService.AddAsync(notificationDto);
        }
    }

    public async Task CancelOrderAsync(CancelOrderDto Model)
    {
        var order = await _queryOrderRepository.GetFirstAsync(x => x.Id == Model.Id)
          .Include(x => x.OrderPreparation)
              .ThenInclude(x => x.Company)
                      .Include(x => x.OrderPreparation)
              .ThenInclude(x => x.Offer)
                  .ThenInclude(x => x.Company)
          .FirstAsync();

        order.Status = OrderStatusEnum.OrderCancelled;

        _updateOrderRepository.Update(order);

        var users = await _userManager.Users.Where(x => x.CompanyId == order.OrderPreparation.Offer.CompanyId && x.RoleId == 3).ToListAsync();

        foreach (var user in users)
        {
            string message = $"{order.OrderPreparation.Company.Name} Firması, {order.OrderCode} Referans Numaralı Siparişi İptal Etti";

            var notificationDto = new NotificationDto(0, user.Id, message, false);

            await _notificationService.AddAsync(notificationDto);
        }
    }

    public async Task DeliveredOrderAsync(DeliveredOrderDto Model)
    {
        var order = await _queryOrderRepository.GetFirstAsync(x => x.Id == Model.Id)
           .Include(x => x.OrderPreparation)
               .ThenInclude(x => x.Company)
                       .Include(x => x.OrderPreparation)
               .ThenInclude(x => x.Offer)
                   .ThenInclude(x => x.Company)
           .FirstAsync();

        order.Status = OrderStatusEnum.Delivered;

        _updateOrderRepository.Update(order);

        var users = await _userManager.Users.Where(x => x.CompanyId == order.OrderPreparation.Offer.CompanyId && x.RoleId == 3).ToListAsync();

        foreach (var user in users)
        {
            string message = $"{order.OrderPreparation.Company.Name} Firması, {order.OrderCode} Referans Numaralı Siparişi Teslim Aldı";

            var notificationDto = new NotificationDto(0, user.Id, message, false);

            await _notificationService.AddAsync(notificationDto);
        }
    }

    public async Task ChangeOrderStatusAsync(ChangeOrderStatusDto Model)
    {
        var order = await _queryOrderRepository.GetFirstAsync(x => x.Id == Model.Id)
           .Include(x => x.OrderPreparation)
               .ThenInclude(x => x.Company)
                       .Include(x => x.OrderPreparation)
               .ThenInclude(x => x.Offer)
                   .ThenInclude(x => x.Company)
           .FirstAsync();

        order.Status = OrderStatusEnum.Delivered;

        _updateOrderRepository.Update(order);

       /* var users = await _userManager.Users.Where(x => x.CompanyId == order.OrderPreparation.Offer.CompanyId && x.RoleId == 3).ToListAsync();

        foreach (var user in users)
        {
            string message = $"Tedarikçi {order.OrderCode} Referans Numaralı Siparişin Statüsünü Güncelle";

            var notificationDto = new NotificationDto(0, user.Id, message, false);

            await _notificationService.AddAsync(notificationDto);
        }
       */
    }
}
