using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.BankInfoRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class BankInfoService : IBankInfoService
{
    private readonly IAddBankInfoRepository _addBankInfoRepository;
    private readonly IUpdateBankInfoRepository _updateBankInfoRepository;
    private readonly IDeleteBankInfoRepository _deleteBankInfoRepository;
    private readonly IQueryBankInfoRepository _queryBankInfoRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public BankInfoService(IAddBankInfoRepository addBankInfoRepository,
        IUpdateBankInfoRepository updateBankInfoRepository,
        IDeleteBankInfoRepository deleteBankInfoRepository,
        IQueryBankInfoRepository queryBankInfoRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addBankInfoRepository = addBankInfoRepository;
        _updateBankInfoRepository = updateBankInfoRepository;
        _deleteBankInfoRepository = deleteBankInfoRepository;
        _queryBankInfoRepository = queryBankInfoRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BankInfoListDto>> GetAllAsync(int companyId, BankInfoFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryBankInfoRepository
            .GetList(x => x.CompanyId == companyId &&
                          (filter == null || filter.Currency == null || x.Currency.Name.ToLower().Contains(filter.Currency.ToLower()) || x.Currency.Code.ToLower().Contains(filter.Currency.ToLower())) &&
                          (filter == null || filter.BranchId == null || x.BranchId == filter.BranchId) &&
                          (filter == null || string.IsNullOrEmpty(filter.IBAN) || x.IBAN.ToLower().Contains(filter.IBAN.ToLower()) &&
                          (filter == null || filter.CityId == null || x.Branch.CityId == filter.CityId) &&
                          (filter == null || filter.DistrictId == null || x.Branch.DistrictId == filter.DistrictId)))
            .AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<BankInfoListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<BankInfoListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, BankInfoDto entity)
    {
        bool exists = await _queryBankInfoRepository.IsExisting(x => x.IBAN == entity.IBAN && x.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<BankInfo>(entity);
        addEntity.CompanyId = companyId;

        await _addBankInfoRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, BankInfoDto entity)
    {
        bool exists = await _queryBankInfoRepository.IsExisting(x => x.IBAN == entity.IBAN && x.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryBankInfoRepository.IsExisting(x => x.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<BankInfo>(entity);

        updateEntity.CompanyId = companyId;

        _updateBankInfoRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryBankInfoRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteBankInfoRepository.RemoveById(id);
    }
}
