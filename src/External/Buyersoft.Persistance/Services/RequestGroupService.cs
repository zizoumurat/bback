using AutoMapper;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Repositories.RequestGroupRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class RequestGroupService : IRequestGroupService
{
    private readonly IAddRequestGroupRepository _addRequestGroupRepository;
    private readonly IQueryRequestGroupRepository _queryRequestGroupRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public RequestGroupService(IAddRequestGroupRepository addRequestGroupRepository,
        IQueryRequestGroupRepository queryRequestGroupRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addRequestGroupRepository = addRequestGroupRepository;
        _queryRequestGroupRepository = queryRequestGroupRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<IList<RequestGroupListDto>> GetAllAsync(int subCategoryId)
    {
        var list = await _queryRequestGroupRepository.GetList(x => x.SubCategoryId == subCategoryId).ToListAsync();

        return _mapper.Map<List<RequestGroupListDto>>(list);
    }
    public async Task AddAsync(int companyId, RequestGroupDto entity)
    {
        bool exists = await _queryRequestGroupRepository.IsExisting(x => x.Name == entity.Name);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<RequestGroup>(entity);

        await _addRequestGroupRepository.AddAsync(addEntity);
    }
}
