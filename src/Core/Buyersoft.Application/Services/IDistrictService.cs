using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Services;
public interface IDistrictService
{
    Task<IList<DistrictDto>> GetAllAsync(int cityId);
}
