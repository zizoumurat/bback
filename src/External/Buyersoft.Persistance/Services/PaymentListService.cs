using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.ApprovalChainRepositories;
using Buyersoft.Domain.Repositories.OrderRepositories;
using Buyersoft.Domain.Repositories.PaymentListRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;

public class PaymentListService : IPaymentListService
{

    private readonly IAddPaymentListRepository _addPaymentListRepository;
    private readonly IUpdatePaymentListRepository _updatePaymentListRepository;
    private readonly IDeletePaymentListRepository _deletePaymentListRepository;
    private readonly IQueryPaymentListRepository _queryPaymentListRepository;
    private readonly IQueryOrderRepository _queryOrderRepository;
    private readonly IUpdateOrderRepository _updateOrderRepository;
    private readonly IQueryApprovalChainRepository _queryApprovalChainRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public PaymentListService(IAddPaymentListRepository addPaymentListRepository, IUpdatePaymentListRepository updatePaymentListRepository, IDeletePaymentListRepository deletePaymentListRepository, IQueryPaymentListRepository queryPaymentListRepository, ILocalizationService localizationService, IMapper mapper, IQueryApprovalChainRepository queryApprovalChainRepository, IUpdateOrderRepository updateOrderRepository, IQueryOrderRepository queryOrderRepository)
    {
        _addPaymentListRepository = addPaymentListRepository;
        _updatePaymentListRepository = updatePaymentListRepository;
        _deletePaymentListRepository = deletePaymentListRepository;
        _queryPaymentListRepository = queryPaymentListRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _queryApprovalChainRepository = queryApprovalChainRepository;
        _updateOrderRepository = updateOrderRepository;
        _queryOrderRepository = queryOrderRepository;
    }

    public async Task AddAsync(int companyId, PaymentListCreateDto entity)
    {
        var orderList = await _queryOrderRepository.GetList(x => entity.OrderIdList.Contains(x.Id)).ToListAsync();

        var paymentListCode = $"{DateTime.Now:MMdd}{new Random().Next(1000, 9999)}";

        var addEntity = new PaymentList()
        {
            CompanyId = companyId,
            Subject = entity.Subject,
            PaymentListCode = paymentListCode,
            Status = Domain.Enums.ApprovalStatus.Pending,
            TotalPrice = orderList.Sum(x => x.TotalPrice),
        };

        await _addPaymentListRepository.AddAsync(addEntity);

        foreach (var order in orderList)
        {
            order.PaymentListId = addEntity.Id;
        }

        _updateOrderRepository.UpdateRange(orderList);

        var relevantApprovalChains = await _queryApprovalChainRepository // harcamaya göre limitlendirilmeli
            .GetList(x => x.CompanyId == companyId)
            .Include(x => x.ApprovalChainUsers)
            .OrderBy(x => x.SpendLimit)
            .ToListAsync();

        var uniqueUsers = new HashSet<int>();

        addEntity.PaymentListApprovals = new List<PaymentListApproval>();

        foreach (var approvalChain in relevantApprovalChains)
        {
            foreach (var item in approvalChain.ApprovalChainUsers)
            {
                // Eğer bu UserId daha önce eklenmemişse, onu ekliyoruz.
                if (uniqueUsers.Add(item.UserId))
                {
                    addEntity.PaymentListApprovals.Add(new PaymentListApproval()
                    {
                        PaymentListId = addEntity.Id,
                        Status = ApprovalStatus.Pending,
                        UserId = item.UserId,
                        Comment = string.Empty
                    });
                }
            }
        }

        _updatePaymentListRepository.Update(addEntity);
    }

    public async Task ApproveRejectPaymentList(int userId, int companyId, ApproveRejectPaymentListDto model)
    {
        var paymentList = await _queryPaymentListRepository.GetFirstAsync(x => x.Id == model.Id).Include(x => x.PaymentListApprovals).FirstOrDefaultAsync();


        var approval = paymentList.PaymentListApprovals.FirstOrDefault(x => x.UserId == userId);

        if (approval != null)
        {
            approval.Status = model.Status;
        }

        if (paymentList.PaymentListApprovals.All(x => x.Status == ApprovalStatus.Approved))
        {
            paymentList.Status = ApprovalStatus.Approved;
        }

        if (paymentList.PaymentListApprovals.Any(x => x.Status == ApprovalStatus.Rejected))
        {
            paymentList.Status = ApprovalStatus.Rejected;
        }

        _updatePaymentListRepository.Update(paymentList);
    }

    public Task DeleteAsync(int id, int companyId)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<PaymentListDto>> GetAllAsync(int companyId, PaymentListFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _queryPaymentListRepository.GetList(x => x.CompanyId == companyId && (filter.Status == null || x.Status == filter.Status))
            .Include(x => x.Orders)
            .Include(x => x.PaymentListApprovals)
            .AsQueryable();

        var count = await query.CountAsync();

        var items = await query
                    .Skip((pagination.Page - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
                    .Select(x => new PaymentListDto()
                    {
                        Id = x.Id,
                        PaymentListCode = x.PaymentListCode,
                        TotalPrice = x.TotalPrice,
                        Status = ApprovalStatus.Pending,
                        Subject = x.Subject,
                        ApprovalUsers = x.PaymentListApprovals.Select(a=> new ApprovalUser(a.UserId, $"{a.User.Name} {a.User.Surname}", a.Status)).ToList(),
                        Orders = x.Orders.Select(o => new OrderListDto(o.Id, o.OrderCode, o.TotalPrice, o.Status,o.OrderDate,
                        o.OrderItems.Select(oi => new OrderItemListDto(oi.Id,oi.OfferDetailId,oi.ProductDefinition,oi.UnitPrice,oi.TotalPrice,oi.Quantity)).ToList(),
                        o.Document != null ? Convert.ToBase64String(o.Document.FileContent) : "",
                        o.Document != null ? o.Document.FileName : "")).ToList()
                    })
                    .ToListAsync();

        return new PaginatedList<PaymentListDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public Task UpdateAsync(int companyId, PaymentListDto entity)
    {
        throw new NotImplementedException();
    }
}
