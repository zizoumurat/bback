using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Repositories.TaxOfficeRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class TaxOfficeService : ITaxOfficeService
{
    private readonly IQueryTaxOfficeRepository _queryTaxOfficeRepository;
    private readonly IMapper _mapper;

    public TaxOfficeService(IQueryTaxOfficeRepository queryTaxOfficeRepository, IMapper mapper)
    {
        _queryTaxOfficeRepository = queryTaxOfficeRepository;
        _mapper = mapper;
    }

    public async Task<IList<TaxOfficeDto>> GetAllAsync(int cityId)
    {
        var list =  await _queryTaxOfficeRepository.GetList(x => x.CityId == cityId).ToListAsync();

        return _mapper.Map<List<TaxOfficeDto>>(list);
    }
}
