using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.BranchRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Buyersoft.Persistance.Services;
public class BranchService : IBranchService
{
    private readonly IQueryBranchRepository _queryBranchRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public BranchService(IQueryBranchRepository queryBranchRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _queryBranchRepository = queryBranchRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BranchListDto>> GetAllAsync(int companyId, BranchFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryBranchRepository
            .GetList(x => (filter == null || string.IsNullOrEmpty(filter.Address) || x.Address.ToLower().Contains(filter.Address.ToLower())) &&
                (filter == null || string.IsNullOrEmpty(filter.BankName) || x.BankName.ToLower().Contains(filter.BankName.ToLower())) &&
                (filter == null || string.IsNullOrEmpty(filter.City) || x.City.Name.ToLower().Contains(filter.City.ToLower())) &&
                (filter == null || string.IsNullOrEmpty(filter.District) || x.District.Name.ToLower().Contains(filter.District.ToLower())) &&
                (filter == null || filter.CityId == null || x.CityId == filter.CityId) &&
                (filter == null || filter.DistrictId == null || x.DistrictId == filter.DistrictId))
                .AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<BranchListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<BranchListDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<IList<SelectListItemDto>> GetSelectListItemsAsync(Dictionary<string, string> Filters)
    {
        var query = _queryBranchRepository.GetList(x => true);

        if (Filters != null)
        {
            foreach (var filter in Filters)
            {
                query = query.Where($"{filter.Key} == @0", filter.Value);
            }
        }

        return await query.Select(x => new SelectListItemDto(x.Id, x.BankName + " / " + x.BranchName)).ToListAsync();
    }
}
