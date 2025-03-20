using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IMainCategorieservice
{
    Task AddAsync(int companyId, MainCategoryDto entity);

    Task UpdateAsync(int companyId, MainCategoryDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<MainCategoryListDto>> GetAllAsync(int companyId, MainCategoryFilterDto filter, PageRequest pagination);
}
