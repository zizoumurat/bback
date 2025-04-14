using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Repositories.DistrictRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class DistrictService : IDistrictService
{
    private readonly IQueryDistrictRepository _queryDistrictRepository;
    private readonly IMapper _mapper;

    public DistrictService(IQueryDistrictRepository queryDistrictRepository, IMapper mapper)
    {
        _queryDistrictRepository = queryDistrictRepository;
        _mapper = mapper;
    }

    public async Task<IList<DistrictDto>> GetAllAsync(int cityId)
    {
        var list =  await _queryDistrictRepository.GetList(x => x.CityId == cityId).ToListAsync();

        return _mapper.Map<List<DistrictDto>>(list);
    }
}
