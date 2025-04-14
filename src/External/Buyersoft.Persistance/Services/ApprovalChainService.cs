using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.ApprovalChainRepositories;
using Buyersoft.Domain.Repositories.ApprovalChainUserRepositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Buyersoft.Persistance.Services;
public class ApprovalChainService : IApprovalChainService
{
    private readonly IAddApprovalChainUserRepository _addApprovalChainUserRepository;
    private readonly IDeleteApprovalChainUserRepository _deleteApprovalChainUserRepository;
    private readonly IQueryApprovalChainUserRepository _queryApprovalChainUserRepository;
    private readonly IAddApprovalChainRepository _addApprovalChainRepository;
    private readonly IUpdateApprovalChainRepository _updateApprovalChainRepository;
    private readonly IDeleteApprovalChainRepository _deleteApprovalChainRepository;
    private readonly IQueryApprovalChainRepository _queryApprovalChainRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public ApprovalChainService(IAddApprovalChainRepository addApprovalChainRepository,
        IUpdateApprovalChainRepository updateApprovalChainRepository,
        IDeleteApprovalChainRepository deleteApprovalChainRepository,
        IQueryApprovalChainRepository queryApprovalChainRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IAddApprovalChainUserRepository addApprovalChainUserRepository,
        IDeleteApprovalChainUserRepository deleteApprovalChainUserRepository,
        IQueryApprovalChainUserRepository queryApprovalChainUserRepository)
    {
        _addApprovalChainRepository = addApprovalChainRepository;
        _updateApprovalChainRepository = updateApprovalChainRepository;
        _deleteApprovalChainRepository = deleteApprovalChainRepository;
        _queryApprovalChainRepository = queryApprovalChainRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _addApprovalChainUserRepository = addApprovalChainUserRepository;
        _deleteApprovalChainUserRepository = deleteApprovalChainUserRepository;
        _queryApprovalChainUserRepository = queryApprovalChainUserRepository;
    }

    public async Task<PaginatedList<ApprovalChainListDto>> GetAllAsync(int companyId, ApprovalChainFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryApprovalChainRepository
            .GetList(x => x.CompanyId == companyId &&
                          (filter == null || filter.CurrencyId == null || x.CurrencyId == filter.CurrencyId) &&
                          (filter == null || filter.UserId == null || (x.ApprovalChainUsers != null && x.ApprovalChainUsers.Any(u => u.UserId == filter.UserId))))
            .AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<ApprovalChainListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<ApprovalChainListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, ApprovalChainDto entity)
    {
        bool exists = await _queryApprovalChainRepository.IsExisting(x => x.SpendLimit == entity.SpendLimit && x.CurrencyId == entity.CurrencyId && x.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<ApprovalChain>(entity);

        addEntity.CompanyId = companyId;

        await _addApprovalChainRepository.AddAsync(addEntity);

        var approvalChainUsers = entity.UserIdList.Select(userId => new ApprovalChainUser
        {
            ApprovalChainId = addEntity.Id,
            UserId = userId
        }).ToList();

        await _addApprovalChainUserRepository.AddRangeAsync(approvalChainUsers);
    }

    public async Task UpdateAsync(int companyId, ApprovalChainDto entity)
    {
        bool exists = await _queryApprovalChainRepository.IsExisting(x => x.SpendLimit == entity.SpendLimit && x.CurrencyId == entity.CurrencyId && x.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryApprovalChainRepository.IsExisting(x => x.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<ApprovalChain>(entity);

        updateEntity.CompanyId = companyId;

        _updateApprovalChainRepository.Update(updateEntity);

        var list = await _queryApprovalChainUserRepository.GetList(x => x.ApprovalChainId == updateEntity.Id).ToListAsync();

        _deleteApprovalChainUserRepository.RemoveRange(list);

        var approvalChainUsers = entity.UserIdList.Select(userId => new ApprovalChainUser
        {
            ApprovalChainId = updateEntity.Id,
            UserId = userId
        }).ToList();

        await _addApprovalChainUserRepository.AddRangeAsync(approvalChainUsers);
    }

    public async Task DeleteAsync(int id, int companyId)
    {

        bool exists = await _queryApprovalChainRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var list = await _queryApprovalChainUserRepository.GetList(x => x.ApprovalChainId == id).ToListAsync();

        _deleteApprovalChainUserRepository.RemoveRange(list);

        _deleteApprovalChainRepository.RemoveById(id);
    }
}
