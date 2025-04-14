using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.OfferLimitRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class OfferLimitService : IOfferLimitService
{
    private readonly IAddOfferLimitRepository _addOfferLimitRepository;
    private readonly IUpdateOfferLimitRepository _updateOfferLimitRepository;
    private readonly IDeleteOfferLimitRepository _deleteOfferLimitRepository;
    private readonly IQueryOfferLimitRepository _queryOfferLimitRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public OfferLimitService(IAddOfferLimitRepository addOfferLimitRepository,
        IUpdateOfferLimitRepository updateOfferLimitRepository,
        IDeleteOfferLimitRepository deleteOfferLimitRepository,
        IQueryOfferLimitRepository queryOfferLimitRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addOfferLimitRepository = addOfferLimitRepository;
        _updateOfferLimitRepository = updateOfferLimitRepository;
        _deleteOfferLimitRepository = deleteOfferLimitRepository;
        _queryOfferLimitRepository = queryOfferLimitRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OfferLimitListDto>> GetAllAsync(int companyId, OfferLimitFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryOfferLimitRepository.GetList(x => x.CompanyId == companyId).AsQueryable();

        if (filter != null)
        {
            query = query.Where(x => filter.CurrencyId == default || x.CurrencyId == filter.CurrencyId);
            query = query.Where(x => filter.SpendLimitMin == default || x.SpendLimit >= filter.SpendLimitMin);
            query = query.Where(x => filter.SpendLimitMax == default || x.SpendLimit <= filter.SpendLimitMax);
            query = query.Where(x => filter.MinimumOfferCountMin == default || x.MinimumOfferCount >= filter.MinimumOfferCountMin);
            query = query.Where(x => filter.MinimumOfferCountMax == default || x.MinimumOfferCount <= filter.MinimumOfferCountMax);
        }

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<OfferLimitListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<OfferLimitListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, OfferLimitDto model)
    {
        bool exists = await _queryOfferLimitRepository.IsExisting(x => x.CompanyId == companyId && (x.SpendLimit == model.SpendLimit || x.MinimumOfferCount == model.SpendLimit));

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<OfferLimit>(model);
        addEntity.CompanyId = companyId;

        await _addOfferLimitRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, OfferLimitDto model)
    {
        bool exists = await _queryOfferLimitRepository.IsExisting(x => (x.CompanyId == companyId && x.Id != model.Id) && 
            (x.SpendLimit == model.SpendLimit || x.MinimumOfferCount == model.MinimumOfferCount));

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryOfferLimitRepository.IsExisting(x => x.CompanyId == companyId && x.Id == model.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<OfferLimit>(model);

        updateEntity.CompanyId = companyId;

        _updateOfferLimitRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryOfferLimitRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteOfferLimitRepository.RemoveById(id);
    }
}
