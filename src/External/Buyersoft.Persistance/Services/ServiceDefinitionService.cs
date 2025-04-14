using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.ServiceDefinitionRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class ServiceDefinitionService : IServiceDefinitionService
{
    private readonly IAddServiceDefinitionRepository _addServiceDefinitionRepository;
    private readonly IUpdateServiceDefinitionRepository _updateServiceDefinitionRepository;
    private readonly IDeleteServiceDefinitionRepository _deleteServiceDefinitionRepository;
    private readonly IQueryServiceDefinitionRepository _queryServiceDefinitionRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public ServiceDefinitionService(IAddServiceDefinitionRepository addServiceDefinitionRepository,
        IUpdateServiceDefinitionRepository updateServiceDefinitionRepository,
        IDeleteServiceDefinitionRepository deleteServiceDefinitionRepository,
        IQueryServiceDefinitionRepository queryServiceDefinitionRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addServiceDefinitionRepository = addServiceDefinitionRepository;
        _updateServiceDefinitionRepository = updateServiceDefinitionRepository;
        _deleteServiceDefinitionRepository = deleteServiceDefinitionRepository;
        _queryServiceDefinitionRepository = queryServiceDefinitionRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ServiceDefinitionDto>> GetAllAsync(int companyId, ServiceDefinitionDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryServiceDefinitionRepository.GetList(x => x.Category.CompanyId == companyId);

        if (filter != null)
            query = query.Where(x => x.CategoryId == filter.CategoryId);

        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<ServiceDefinitionDto>(_mapper.ConfigurationProvider)
        .ToListAsync();


        return new PaginatedList<ServiceDefinitionDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, ServiceDefinitionDto entity)
    {
        bool exists = await _queryServiceDefinitionRepository.IsExisting(x => x.Definition == entity.Definition && x.Category.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<ServiceDefinition>(entity);

        await _addServiceDefinitionRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, ServiceDefinitionDto entity)
    {
        bool exists = await _queryServiceDefinitionRepository.IsExisting(x => x.Definition == entity.Definition && x.Category.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryServiceDefinitionRepository.IsExisting(x => x.Category.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<ServiceDefinition>(entity);

        _updateServiceDefinitionRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryServiceDefinitionRepository.IsExisting(x => x.Id == id && x.Category.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteServiceDefinitionRepository.RemoveById(id);
    }

    public async Task<IList<ServiceDefinitionDto>> GetAllWithOutPaginationAsync(int companyId)
    {
        var list = await _queryServiceDefinitionRepository.GetList(x => x.Category.CompanyId == companyId).ToListAsync();

        return _mapper.Map<List<ServiceDefinitionDto>>(list);
    }
}
