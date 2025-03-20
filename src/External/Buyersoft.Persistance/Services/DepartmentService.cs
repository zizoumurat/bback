using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.DepartmentRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class DepartmentService : IDepartmentService
{
    private readonly IAddDepartmentRepository _addDepartmentRepository;
    private readonly IUpdateDepartmentRepository _updateDepartmentRepository;
    private readonly IDeleteDepartmentRepository _deleteDepartmentRepository;
    private readonly IQueryDepartmentRepository _queryDepartmentRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public DepartmentService(IAddDepartmentRepository addDepartmentRepository,
        IUpdateDepartmentRepository updateDepartmentRepository,
        IDeleteDepartmentRepository deleteDepartmentRepository,
        IQueryDepartmentRepository queryDepartmentRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addDepartmentRepository = addDepartmentRepository;
        _updateDepartmentRepository = updateDepartmentRepository;
        _deleteDepartmentRepository = deleteDepartmentRepository;
        _queryDepartmentRepository = queryDepartmentRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DepartmentListDto>> GetAllAsync(int companyId, DepartmentFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _queryDepartmentRepository.GetList(x => x.CompanyId == companyId &&
                    (filter == null || string.IsNullOrEmpty(filter.Name) ||
                    x.Name.Contains(filter.Name))).AsQueryable();

        var count = await query.CountAsync();
        var items = await query
                    .Skip((pagination.Page - 1) * pagination.PageSize)
                    .Take(pagination.PageSize)
                    .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
                    .ProjectTo<DepartmentListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        return new PaginatedList<DepartmentListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, DepartmentDto entity)
    {
        bool exists = await _queryDepartmentRepository.IsExisting(x => x.Name == entity.Name && x.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<Department>(entity);
        addEntity.CompanyId = companyId;

        await _addDepartmentRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, DepartmentDto entity)
    {
        bool exists = await _queryDepartmentRepository.IsExisting(x => x.Name == entity.Name && x.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryDepartmentRepository.IsExisting(x => x.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<Department>(entity);

        updateEntity.CompanyId = companyId;

        _updateDepartmentRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryDepartmentRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteDepartmentRepository.RemoveById(id);
    }

    public async Task<IList<SelectListItemDto>> GetSelectListItemAsync(int companyId)
    {
        var list = await _queryDepartmentRepository.GetList(x => x.CompanyId == companyId)
            .Select(x => new SelectListItemDto(x.Id, x.Name))
            .ToListAsync();

        return list;
    }
}
