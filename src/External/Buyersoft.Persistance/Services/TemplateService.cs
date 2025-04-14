using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.TemplateRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class TemplateService : ITemplateService
{
    private readonly IAddTemplateRepository _addTemplateRepository;
    private readonly IUpdateTemplateRepository _updateTemplateRepository;
    private readonly IDeleteTemplateRepository _deleteTemplateRepository;
    private readonly IQueryTemplateRepository _queryTemplateRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public TemplateService(IAddTemplateRepository addTemplateRepository,
        IUpdateTemplateRepository updateTemplateRepository,
        IDeleteTemplateRepository deleteTemplateRepository,
        IQueryTemplateRepository queryTemplateRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addTemplateRepository = addTemplateRepository;
        _updateTemplateRepository = updateTemplateRepository;
        _deleteTemplateRepository = deleteTemplateRepository;
        _queryTemplateRepository = queryTemplateRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TemplateListDto>> GetAllAsync(int companyId, TemplateFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryTemplateRepository.GetList(x => (x.CompanyId == companyId || x.Requests.Any(r=>r.Offers.Any(xx=>xx.CompanyId == companyId))) && (filter == null || string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name))).AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize) .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<TemplateListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<TemplateListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, TemplateDto entity)
    {
        bool exists = await _queryTemplateRepository.IsExisting(x => x.Name == entity.Name && x.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<Template>(entity);
        addEntity.CompanyId = companyId;

        await _addTemplateRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, TemplateDto entity)
    {
        bool exists = await _queryTemplateRepository.IsExisting(x => x.Name == entity.Name && x.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryTemplateRepository.IsExisting(x => x.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<Template>(entity);

        updateEntity.CompanyId = companyId;

        _updateTemplateRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryTemplateRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteTemplateRepository.RemoveById(id);
    }

    public async Task<IList<TemplateListDto>> GetAllByRequestGroupAsync(int requestGroupId)
    {
        var templateList = await _queryTemplateRepository.GetList(x => x.RequestGroupId == requestGroupId).ToListAsync();

        return _mapper.Map<List<TemplateListDto>>(templateList);
    }

    public async Task<TemplateListDto> GetById(int Id)
    {

        bool exists = await _queryTemplateRepository.IsExisting(x => x.Id == Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var template = await _queryTemplateRepository.GetByIdAsync(Id);

        return _mapper.Map<TemplateListDto>(template);
    }
}
