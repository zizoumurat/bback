using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.CompanyRequestGroupRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class CompanyRequestGroupService : ICompanyRequestGroupService
{
    private readonly IAddCompanyRequestGroupRepository _addCompanyRequestGroupRepository;
    private readonly IQueryCompanyRequestGroupRepository _queryCompanyRequestGroupRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public CompanyRequestGroupService(IAddCompanyRequestGroupRepository addCompanyRequestGroupRepository,
        IQueryCompanyRequestGroupRepository queryCompanyRequestGroupRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addCompanyRequestGroupRepository = addCompanyRequestGroupRepository;
        _queryCompanyRequestGroupRepository = queryCompanyRequestGroupRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<IList<CompanyRequestGroupListDto>> GetAllAsync(int companyId, int subCategoryId)
    {
        var list = await _queryCompanyRequestGroupRepository.GetList(x => x.RequestGroup.SubCategory.CompanySubCategory.CompanyId == companyId
            && x.RequestGroup.SubCategory.CompanySubCategory.Id == subCategoryId).ToListAsync();

        return _mapper.Map<List<CompanyRequestGroupListDto>>(list);
    }
    public async Task AddAsync(int companyId, CompanyRequestGroupDto entity)
    {
        bool exists = await _queryCompanyRequestGroupRepository.IsExisting(x => x.Name == entity.Name);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<CompanyRequestGroup>(entity);
        addEntity.CompanyId = companyId;

        await _addCompanyRequestGroupRepository.AddAsync(addEntity);
    }
}
