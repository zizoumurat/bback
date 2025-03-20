using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.BudgetRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class BudgetService : IBudgetService
{
    private readonly IAddBudgetRepository _addBudgetRepository;
    private readonly IUpdateBudgetRepository _updateBudgetRepository;
    private readonly IDeleteBudgetRepository _deleteBudgetRepository;
    private readonly IQueryBudgetRepository _queryBudgetRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public BudgetService(IAddBudgetRepository addBudgetRepository,
        IUpdateBudgetRepository updateBudgetRepository,
        IDeleteBudgetRepository deleteBudgetRepository,
        IQueryBudgetRepository queryBudgetRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addBudgetRepository = addBudgetRepository;
        _updateBudgetRepository = updateBudgetRepository;
        _deleteBudgetRepository = deleteBudgetRepository;
        _queryBudgetRepository = queryBudgetRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BudgetListDto>> GetAllAsync(int companyId, BudgetFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryBudgetRepository.GetList(x => x.CompanyId == companyId && !x.IsDeleted);

        if (filter != null)
        {
            query = query.Where(x =>
                (string.IsNullOrEmpty(filter.BudgetTitle) || x.BudgetTitle.ToLower().Contains(filter.BudgetTitle.ToLower())) &&
                (filter.DepartmentId == default || x.DepartmentId == filter.DepartmentId) &&
                (filter.StartDate == default || x.StartDate >= filter.StartDate) &&
                (filter.EndDate == default || x.EndDate <= filter.EndDate) &&
                (filter.BudgetLimitMax == default || x.BudgetLimit <= filter.BudgetLimitMax) &&
                (filter.BudgetLimitMin == default || x.BudgetLimit >= filter.BudgetLimitMin) &&
                (filter.CurrencyId == default || x.CurrencyId == filter.CurrencyId)
            );
        }

        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<BudgetListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<BudgetListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, BudgetDto entity)
    {
        bool exists = await _queryBudgetRepository.IsExisting(x => x.BudgetTitle == entity.BudgetTitle && x.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<Budget>(entity);
        addEntity.CompanyId = companyId;
        addEntity.AvailableLimit = addEntity.BudgetLimit;

        await _addBudgetRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, BudgetDto entity)
    {
        bool exists = await _queryBudgetRepository.IsExisting(x => x.BudgetTitle == entity.BudgetTitle && x.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var existEntity = await _queryBudgetRepository.GetFirstAsync(x => x.CompanyId == companyId && x.Id == entity.Id).FirstAsync();

        if (existEntity == null)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        if (existEntity.AvailableLimit < entity.AvailableLimit)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("ExistingAvailableLimitError"));
        }

        var limitDifference = entity.BudgetLimit - existEntity.BudgetLimit;

        var updateEntity = _mapper.Map<Budget>(entity);

        updateEntity.CompanyId = companyId;
        updateEntity.AvailableLimit = existEntity.AvailableLimit + limitDifference;

        _updateBudgetRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryBudgetRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteBudgetRepository.RemoveById(id);
    }

    public async Task<IList<BudgetListDto>> GetAvailableList(int companyId)
    {
        var dateNow = DateOnly.FromDateTime(DateTime.Now);

        var list = await _queryBudgetRepository
            .GetList(x => x.CompanyId == companyId && x.AvailableLimit > 0 && x.StartDate <= dateNow && x.EndDate >= dateNow)
            .ProjectTo<BudgetListDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;
    }
}
