using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface ICityService
{
    Task<IList<CityDto>> GetAllAsync();
}
