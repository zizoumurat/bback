using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Microsoft.AspNetCore.Http;

namespace Buyersoft.Application.Services;
public interface ICategoryService
{
    Task AddAsync(int companyId, CategoryDto entity);

    Task UpdateAsync(int companyId, CategoryDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<CategoryListDto>> GetAllAsync(int companyId, CategoryFilterDto filter, PageRequest pagination);
    Task<byte[]> GetExcellData(int companyId, CategoryFilterDto filter);
    Task ImportExcellAsync(int companyId, IFormFile excelFile);

    Task<CategoryListDto> GetCategoryIdByParameters(int companyId, CategoryGroupFilter filter);
}
