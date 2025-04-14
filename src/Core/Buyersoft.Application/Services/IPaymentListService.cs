using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IPaymentListService
{
    Task AddAsync(int companyId, PaymentListCreateDto entity);

    Task ApproveRejectPaymentList(int userId, int companyId, ApproveRejectPaymentListDto model);

    Task UpdateAsync(int companyId, PaymentListDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<PaymentListDto>> GetAllAsync(int companyId, PaymentListFilterDto filter, PageRequest pagination);
}
