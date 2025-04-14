using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface ICurrencyService
{
    Task<IList<CurrencyDto>> GetAllAsync();
}
