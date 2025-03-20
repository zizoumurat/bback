using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IBudgetService
{
    Task AddAsync(int companyId, BudgetDto entity);

    Task UpdateAsync(int companyId, BudgetDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<BudgetListDto>> GetAllAsync(int companyId, BudgetFilterDto filter, PageRequest pagination);
    Task<IList<BudgetListDto>> GetAvailableList(int companyId);
}
