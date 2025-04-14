using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Repositories.CurrencyRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class CurrencyService : ICurrencyService
{
    private readonly IQueryCurrencyRepository _queryCurrencyRepository;
    private readonly IMapper _mapper;

    public CurrencyService(IQueryCurrencyRepository queryCurrencyRepository, IMapper mapper)
    {
        _queryCurrencyRepository = queryCurrencyRepository;
        _mapper = mapper;
    }

    public async Task<IList<CurrencyDto>> GetAllAsync()
    {
        var list =  await _queryCurrencyRepository.GetList(x => true).ToListAsync();

        return _mapper.Map<List<CurrencyDto>>(list);
    }
}
