using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.OfferRepositories;
using Buyersoft.Domain.Repositories.OrderPreparationRepositories;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class OrderPreparationService : IOrderPreparationService
{
    private readonly IAddOrderPreparationRepository _addOrderPreparationRepository;
    private readonly IUpdateOrderPreparationRepository _updateOrderPreparationRepository;
    private readonly IDeleteOrderPreparationRepository _deleteOrderPreparationRepository;
    private readonly IQueryOrderPreparationRepository _queryOrderPreparationRepository;
    private readonly IQueryRequestRepository _queryRequestRepository;
    private readonly IQueryOfferRepository _queryOfferRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public OrderPreparationService(IAddOrderPreparationRepository addOrderPreparationRepository,
        IUpdateOrderPreparationRepository updateOrderPreparationRepository,
        IDeleteOrderPreparationRepository deleteOrderPreparationRepository,
        IQueryOrderPreparationRepository queryOrderPreparationRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IQueryRequestRepository queryRequestRepository,
        IQueryOfferRepository queryOfferRepository)
    {
        _addOrderPreparationRepository = addOrderPreparationRepository;
        _updateOrderPreparationRepository = updateOrderPreparationRepository;
        _deleteOrderPreparationRepository = deleteOrderPreparationRepository;
        _queryOrderPreparationRepository = queryOrderPreparationRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _queryRequestRepository = queryRequestRepository;
        _queryOfferRepository = queryOfferRepository;
    }

    public async Task AddAsync(int companyId, int RequestId, int OfferId)
    {
        var offer = await _queryOfferRepository.GetFirstAsync(x => x.Id == OfferId)
            .Include(x => x.Request)
                .ThenInclude(x => x.Template)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.MainCategory)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.SubCategory)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.RequestGroup)

            .FirstAsync();

        OrderPreparation addEntity = new()
        {
            CompanyId = companyId,
            RequestId = RequestId,
            OfferId = OfferId,
            MainCategory = offer.Request.Category.MainCategory.Name,
            SubCategory = offer.Request.Category.SubCategory.Name,
            RequestGroup = offer.Request.Category.RequestGroup.Name,
            RequestCode = offer.Request.RequestCode,
            ReferenceCode = offer.ReferenceCode,
            AvailableLimit = true
        };

        await _addOrderPreparationRepository.AddAsync(addEntity);
    }

    public async Task CreateOrder(OrderCreateDto Model)
    {
        var orderPreparation = await _queryOrderPreparationRepository.GetFirstAsync(x => x.Id == Model.OrderPreparationId)
             .Include(x => x.Orders)
                 .ThenInclude(x => x.OrderItems)
             .Include(x => x.Offer)
                .ThenInclude(x => x.OfferDetails)
             .FirstOrDefaultAsync();

        if (orderPreparation == null)
        {
            throw new Exception("OrderPreparation not found");
        }

        if (Model.OrderItems.Sum(x => x.Quantity) == 0)
        {
            throw new Exception("EmptyOrder");
        }


        var order = new Order()
        {
            OrderPreparationId = Model.OrderPreparationId,
            OrderCode = $"{DateTime.Now:MMdd}{new Random().Next(1000, 9999)}",
            Status = OrderStatusEnum.OrderPending,
            TotalPrice = Model.OrderItems.Sum(x => x.Quantity * x.UnitPrice),
            OrderDate = DateTime.Now,
            OrderItems = Model.OrderItems.Where(x => x.Quantity > 0).Select(x => new OrderItem()
            {
                OfferDetailId = x.OfferDetailId,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.Quantity * x.UnitPrice,
                ProductDefinition = x.ProductDefinition
            }).ToList()
        };
        orderPreparation.Orders.Add(order);

        foreach (var item in orderPreparation.Orders)
        {
            foreach (var offerDetailGroup in item.OrderItems.GroupBy(x => x.OfferDetailId))
            {
                int totalQuantityForOrderItems = offerDetailGroup.Sum(x => x.Quantity);
                int quantityForOfferDetails = orderPreparation.Offer.OfferDetails.First(x => x.Id == offerDetailGroup.Key).Quantity;

                if (totalQuantityForOrderItems > quantityForOfferDetails)
                {
                    throw new Exception("Sipariş miktari olması gerekenden fazla olduğu için işlem gerçekleştirilemedi.");
                }
            }
        }

        var totalOrderPrice = orderPreparation.Orders.Sum(x => x.TotalPrice);
        orderPreparation.TotalPrice = totalOrderPrice;
        orderPreparation.AvailableLimit = true;
        _updateOrderPreparationRepository.Update(orderPreparation);
    }

    public async Task<PaginatedList<OrderPreparationListDto>> GetAllAsync(int companyId, OrderPreparationFilterDto filter, PageRequest pagination)
    {
        var query = _queryOrderPreparationRepository.GetList(x => x.CompanyId == companyId)
            .Include(x => x.Request)
                .ThenInclude(x => x.Category)
            .Include(x => x.Request)
                .ThenInclude(x => x.Currency)
            .Include(x => x.Orders)
                .ThenInclude(x => x.OrderItems)
                    .ThenInclude(x => x.OfferDetail)
            .Include(x => x.Offer)
                .ThenInclude(x => x.OfferDetails)
            .Include(x => x.Offer)
                .ThenInclude(x => x.Company)
            .Select(x => new OrderPreparationListDto()
            {
                Id = x.Id,
                RequestId = x.RequestId,
                OfferId = x.OfferId,
                Supplier = x.Offer.Company.Name,
                MainCategory = x.MainCategory,
                SubCategory = x.SubCategory,
                RequestGroup = x.RequestGroup,
                RequestCode = x.RequestCode,
                CurrencyCode = x.Request.Currency.Code,
                ReferenceCode = x.ReferenceCode,
                Unit = x.Request.Category.Unit,
                OrderCount = x.Orders.Count,
                TotalPrice = x.TotalPrice,
                AvailableLimit = x.AvailableLimit,
                OfferDetailList = x.Offer.OfferDetails.Select(od => new OfferDetailListDto(
                    od.Id,
                    od.ProductDefinition,
                    od.UnitPrice,
                    od.Quantity - x.Orders
                        .SelectMany(o => o.OrderItems) // Tüm OrderItem'ları düzleştir
                        .Where(oi => oi.OfferDetailId == od.Id) // Sadece ilgili OfferDetailId'leri filtrele
                        .Sum(oi => oi.Quantity) // Toplam Quantity'yi hesapla
                )).ToList(),
                Orders = x.Orders.Select(or => new OrderListDto(
                    or.Id,
                    or.OrderCode,
                    or.TotalPrice,
                    or.Status,
                    or.OrderDate,
                    or.OrderItems.Select(oi => new OrderItemListDto(
                        oi.Id,
                        oi.OfferDetailId,
                        oi.ProductDefinition,
                        oi.UnitPrice,
                        oi.TotalPrice,
                        oi.Quantity
                    )).ToList()
                )).ToList()
            })
            .AsQueryable();


        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
        .ToListAsync();


        return new PaginatedList<OrderPreparationListDto>(items, count, pagination.Page, pagination.PageSize);

    }

}
