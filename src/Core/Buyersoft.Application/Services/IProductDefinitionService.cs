using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IProductDefinitionService
{
    Task AddAsync(int companyId, ProductDefinitionDto entity);

    Task UpdateAsync(int companyId, ProductDefinitionDto entity);

    Task DeleteAsync(int id, int companyId);
    Task<PaginatedList<ProductDefinitionDto>> GetAllAsync(int companyId,ProductDefinitionDto filter, PageRequest pagination);
}
