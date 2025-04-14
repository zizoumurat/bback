using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.CurrencyParameterRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class CurrencyParameterService : ICurrencyParameterService
{
    private readonly IAddCurrencyParameterRepository _addCurrencyParameterRepository;
    private readonly IUpdateCurrencyParameterRepository _updateCurrencyParameterRepository;
    private readonly IDeleteCurrencyParameterRepository _deleteCurrencyParameterRepository;
    private readonly IQueryCurrencyParameterRepository _queryCurrencyParameterRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public CurrencyParameterService(IAddCurrencyParameterRepository addCurrencyParameterRepository,
        IUpdateCurrencyParameterRepository updateCurrencyParameterRepository,
        IDeleteCurrencyParameterRepository deleteCurrencyParameterRepository,
        IQueryCurrencyParameterRepository queryCurrencyParameterRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addCurrencyParameterRepository = addCurrencyParameterRepository;
        _updateCurrencyParameterRepository = updateCurrencyParameterRepository;
        _deleteCurrencyParameterRepository = deleteCurrencyParameterRepository;
        _queryCurrencyParameterRepository = queryCurrencyParameterRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CurrencyParameterListDto>> GetAllAsync(int companyId, CurrencyParameterFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryCurrencyParameterRepository.GetList(x => x.CompanyId == companyId).AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<CurrencyParameterListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<CurrencyParameterListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, CurrencyParameterDto entity)
    {

        bool exists = await _queryCurrencyParameterRepository.IsExisting(x => x.Currency1.Id == entity.Currency1Id 
            && x.Currency2Id == entity.Currency2Id
            && x.StartDate == entity.ExpiredDate 
            && x.ExpiredDate == entity.ExpiredDate
            && x.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<CurrencyParameter>(entity);
        addEntity.CompanyId = companyId;

        await _addCurrencyParameterRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, CurrencyParameterDto entity)
    {
        bool exists = await _queryCurrencyParameterRepository.IsExisting(x => x.Currency1Id == entity.Currency1Id &&
             x.Currency2Id == entity.Currency2Id && x.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryCurrencyParameterRepository.IsExisting(x => x.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<CurrencyParameter>(entity);

        updateEntity.CompanyId = companyId;

        _updateCurrencyParameterRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryCurrencyParameterRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteCurrencyParameterRepository.RemoveById(id);
    }

    public async Task<IList<ExchangeRateDto>> GetCurrencyExchangeRates(int companyId, int Id)
    {
        var left = await _queryCurrencyParameterRepository
            .GetList(x => x.CompanyId == companyId && x.Currency1Id == Id && x.Currency2Id != Id)
            .Include(x => x.Currency2)
            .Select(x => new ExchangeRateDto($"{x.Currency2.Code}/{x.Currency1.Code}", x.ExchangeRate)).
            ToListAsync();

        var right = await _queryCurrencyParameterRepository
            .GetList(x => x.CompanyId == companyId && x.Currency2Id == Id && x.Currency1Id != Id)
            .Include(x => x.Currency2)
            .Select(x => new ExchangeRateDto($"{x.Currency1.Code}/{x.Currency2.Code}", x.ExchangeRate)).
            ToListAsync();

        var combinedResults = left.Concat(right).ToList();

        return combinedResults;
    }
}
