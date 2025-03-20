using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Repositories.CityRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class CityService : ICityService
{
    private readonly IQueryCityRepository _queryCityRepository;
    private readonly IMapper _mapper;

    public CityService(IQueryCityRepository queryCityRepository, IMapper mapper)
    {
        _queryCityRepository = queryCityRepository;
        _mapper = mapper;
    }

    public async Task<IList<CityDto>> GetAllAsync()
    {
        var list =  await _queryCityRepository.GetList(x => true).ToListAsync();

        return _mapper.Map<List<CityDto>>(list);
    }
}
