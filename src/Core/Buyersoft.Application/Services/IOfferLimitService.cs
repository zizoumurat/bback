using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IOfferLimitService
{
    Task AddAsync(int companyId, OfferLimitDto entity);

    Task UpdateAsync(int companyId, OfferLimitDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<OfferLimitListDto>> GetAllAsync(int companyId, OfferLimitFilterDto filter, PageRequest pagination);
}
