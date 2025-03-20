using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface ICurrencyParameterService
{
    Task AddAsync(int companyId, CurrencyParameterDto entity);

    Task UpdateAsync(int companyId, CurrencyParameterDto entity);

    Task DeleteAsync(int id, int companyId);

    Task<PaginatedList<CurrencyParameterListDto>> GetAllAsync(int companyId, CurrencyParameterFilterDto filter, PageRequest pagination);

    Task<IList<ExchangeRateDto>> GetCurrencyExchangeRates(int companyId, int Id);
}
