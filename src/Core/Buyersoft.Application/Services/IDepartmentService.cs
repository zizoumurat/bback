using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IDepartmentService
{
    Task AddAsync(int companyId, DepartmentDto entity);

    Task UpdateAsync(int companyId, DepartmentDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<IList<SelectListItemDto>> GetSelectListItemAsync(int companyId);

    Task<PaginatedList<DepartmentListDto>> GetAllAsync(int companyId, DepartmentFilterDto filter, PageRequest pagination);
}
