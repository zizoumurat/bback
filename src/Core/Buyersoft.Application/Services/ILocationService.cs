using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface ILocationService
{
    Task AddAsync(int companyId, LocationDto entity);

    Task UpdateAsync(int companyId, LocationDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<LocationListDto>> GetAllAsync(int companyId, LocationFilterDto filter, PageRequest pagination);
    Task<IList<LocationListDto>> GetAllWithOutPaginationAsync(int companyId);
}
