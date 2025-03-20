using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.MainCategoryRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class MainCategorieservice : IMainCategorieservice
{
    private readonly IAddMainCategoryRepository _addMainCategoryRepository;
    private readonly IUpdateMainCategoryRepository _updateMainCategoryRepository;
    private readonly IDeleteMainCategoryRepository _deleteMainCategoryRepository;
    private readonly IQueryMainCategoryRepository _queryMainCategoryRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public MainCategorieservice(IAddMainCategoryRepository addMainCategoryRepository,
        IUpdateMainCategoryRepository updateMainCategoryRepository,
        IDeleteMainCategoryRepository deleteMainCategoryRepository,
        IQueryMainCategoryRepository queryMainCategoryRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addMainCategoryRepository = addMainCategoryRepository;
        _updateMainCategoryRepository = updateMainCategoryRepository;
        _deleteMainCategoryRepository = deleteMainCategoryRepository;
        _queryMainCategoryRepository = queryMainCategoryRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MainCategoryListDto>> GetAllAsync(int companyId, MainCategoryFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryMainCategoryRepository.GetList(x => 
        (filter == null || string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name))).AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<MainCategoryListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<MainCategoryListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, MainCategoryDto entity)
    {
        bool exists = await _queryMainCategoryRepository.IsExisting(x => x.Name == entity.Name);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<MainCategory>(entity);

        await _addMainCategoryRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, MainCategoryDto entity)
    {
        bool exists = await _queryMainCategoryRepository.IsExisting(x => x.Name == entity.Name && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryMainCategoryRepository.IsExisting(x=> x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<MainCategory>(entity);


        _updateMainCategoryRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryMainCategoryRepository.IsExisting(x => x.Id == id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteMainCategoryRepository.RemoveById(id);
    }
}
