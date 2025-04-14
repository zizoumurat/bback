using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.MainCategoryRepositories;
using Buyersoft.Domain.Repositories.CompanySubCategoryRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class CompanySubCategoryService : ICompanySubCategoryService
{
    private readonly IAddCompanySubCategoryRepository _addCompanySubCategoryRepository;
    private readonly IQueryCompanySubCategoryRepository _queryCompanySubCategoryRepository;
    private readonly IQueryMainCategoryRepository _queryMainCategoryRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public CompanySubCategoryService(IAddCompanySubCategoryRepository addCompanySubCategoryRepository,
        IQueryCompanySubCategoryRepository queryCompanySubCategoryRepository,
        IQueryMainCategoryRepository queryMainCategoryRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addCompanySubCategoryRepository = addCompanySubCategoryRepository;
        _queryCompanySubCategoryRepository = queryCompanySubCategoryRepository;
        _queryMainCategoryRepository = queryMainCategoryRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<IList<CompanySubCategoryListDto>> GetAllAsync(int companyId, int mainCategoryId)
    {
        var list = await _queryCompanySubCategoryRepository.GetList(x => x.SubCategory.MainCategoryId == mainCategoryId).ToListAsync();

        return _mapper.Map<List<CompanySubCategoryListDto>>(list);
    }
    public async Task AddAsync(int companyId, CompanySubCategoryDto entity)
    {
        bool exists = await _queryCompanySubCategoryRepository.IsExisting(x => x.Name == entity.Name && x.SubCategoryId == entity.SubCategoryId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<CompanySubCategory>(entity);
        addEntity.CompanyId = companyId;

        await _addCompanySubCategoryRepository.AddAsync(addEntity);
    }
}
