using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IApprovalChainService
{
    Task AddAsync(int companyId, ApprovalChainDto entity);

    Task UpdateAsync(int companyId, ApprovalChainDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<ApprovalChainListDto>> GetAllAsync(int companyId, ApprovalChainFilterDto filter, PageRequest pagination);
}
