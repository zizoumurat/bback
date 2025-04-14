using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.MainCategoryRepositories;
using Buyersoft.Domain.Repositories.SubCategoryRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class SubCategoryService : ISubCategoryService
{
    private readonly IAddSubCategoryRepository _addSubCategoryRepository;
    private readonly IQuerySubCategoryRepository _querySubCategoryRepository;
    private readonly IQueryMainCategoryRepository _queryMainCategoryRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public SubCategoryService(IAddSubCategoryRepository addSubCategoryRepository,
        IQuerySubCategoryRepository querySubCategoryRepository,
        IQueryMainCategoryRepository queryMainCategoryRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addSubCategoryRepository = addSubCategoryRepository;
        _querySubCategoryRepository = querySubCategoryRepository;
        _queryMainCategoryRepository = queryMainCategoryRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<IList<SubCategoryListDto>> GetAllAsync(int companyId, int mainCategoryId)
    {
        var list = await _querySubCategoryRepository.GetList(x => x.MainCategoryId == mainCategoryId).ToListAsync();

        return _mapper.Map<List<SubCategoryListDto>>(list);
    }
    public async Task AddAsync(int companyId, SubCategoryDto entity)
    {
        bool mainCategoryExists = await _queryMainCategoryRepository.IsExisting(x => x.Id == entity.MainCategoryId);

        if (!mainCategoryExists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("MainCategoryNotFound"));
        }

        bool exists = await _querySubCategoryRepository.IsExisting(x => x.Name == entity.Name && x.MainCategoryId == entity.MainCategoryId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<SubCategory>(entity);

        await _addSubCategoryRepository.AddAsync(addEntity);
    }
}
