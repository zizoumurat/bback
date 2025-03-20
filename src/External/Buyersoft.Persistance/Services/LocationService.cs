using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.LocationRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class LocationService : ILocationService
{
    private readonly IAddLocationRepository _addLocationRepository;
    private readonly IUpdateLocationRepository _updateLocationRepository;
    private readonly IDeleteLocationRepository _deleteLocationRepository;
    private readonly IQueryLocationRepository _queryLocationRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public LocationService(IAddLocationRepository addLocationRepository,
        IUpdateLocationRepository updateLocationRepository,
        IDeleteLocationRepository deleteLocationRepository,
        IQueryLocationRepository queryLocationRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addLocationRepository = addLocationRepository;
        _updateLocationRepository = updateLocationRepository;
        _deleteLocationRepository = deleteLocationRepository;
        _queryLocationRepository = queryLocationRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LocationListDto>> GetAllAsync(int companyId, LocationFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _queryLocationRepository.GetList(x => x.CompanyId == companyId);

        if (filter != null)
        {
            query = query.Where(x =>
                (string.IsNullOrEmpty(filter.Name) || x.Name.ToLower().Contains(filter.Name.ToLower())) &&
                (string.IsNullOrEmpty(filter.Address) || x.Address.ToLower().Contains(filter.Address.ToLower())) &&
                (filter.CityId == default || x.CityId == filter.CityId) &&
                (filter.DistrictId == default || x.DistrictId == filter.DistrictId)
            );
        }

        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<LocationListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();


        return new PaginatedList<LocationListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, LocationDto entity)
    {
        bool exists = await _queryLocationRepository.IsExisting(x => x.Name == entity.Name && x.CompanyId == companyId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<Location>(entity);
        addEntity.CompanyId = companyId;

        await _addLocationRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(int companyId, LocationDto entity)
    {
        bool exists = await _queryLocationRepository.IsExisting(x => x.Name == entity.Name && x.CompanyId == companyId && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryLocationRepository.IsExisting(x => x.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<Location>(entity);

        updateEntity.CompanyId = companyId;

        _updateLocationRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryLocationRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteLocationRepository.RemoveById(id);
    }

    public async Task<IList<LocationListDto>> GetAllWithOutPaginationAsync(int companyId)
    {
        var list = await _queryLocationRepository.GetList(x => x.CompanyId == companyId).ToListAsync();

        return _mapper.Map<List<LocationListDto>>(list);
    }
}
