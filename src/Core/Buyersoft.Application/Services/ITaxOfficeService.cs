using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface ITaxOfficeService
{
    Task<IList<TaxOfficeDto>> GetAllAsync(int cityId);
}
