using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.SystemNotificationRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class SystemNotificationService : ISystemNotificationService
{
    private readonly IAddSystemNotificationRepository _addSystemNotificationRepository;
    private readonly IUpdateSystemNotificationRepository _updateSystemNotificationRepository;
    private readonly IDeleteSystemNotificationRepository _deleteSystemNotificationRepository;
    private readonly IQuerySystemNotificationRepository _querySystemNotificationRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;

    public SystemNotificationService(IAddSystemNotificationRepository addSystemNotificationRepository,
        IUpdateSystemNotificationRepository updateSystemNotificationRepository,
        IDeleteSystemNotificationRepository deleteSystemNotificationRepository,
        IQuerySystemNotificationRepository querySystemNotificationRepository,
        ILocalizationService localizationService,
        IMapper mapper)
    {
        _addSystemNotificationRepository = addSystemNotificationRepository;
        _updateSystemNotificationRepository = updateSystemNotificationRepository;
        _deleteSystemNotificationRepository = deleteSystemNotificationRepository;
        _querySystemNotificationRepository = querySystemNotificationRepository;
        _localizationService = localizationService;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SystemNotificationListDto>> GetAllAsync(SystemNotificationFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();
        var query = _querySystemNotificationRepository.GetList(x=> (filter == null || string.IsNullOrEmpty(filter.Message) || x.Message.Contains(filter.Message)) ||
            (filter == null || string.IsNullOrEmpty(filter.Code) || x.Message.Contains(filter.Code)) ||
            (filter == null || filter.Type == null || x.Type == filter.Type)).AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize) .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<SystemNotificationListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<SystemNotificationListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(SystemNotificationDto entity)
    {
        bool exists = await _querySystemNotificationRepository.IsExisting(x => x.Message == entity.Message);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        var addEntity = _mapper.Map<SystemNotification>(entity);

        await _addSystemNotificationRepository.AddAsync(addEntity);
    }

    public async Task UpdateAsync(SystemNotificationDto entity)
    {
        bool exists = await _querySystemNotificationRepository.IsExisting(x => x.Message == entity.Message && x.Id != entity.Id);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _querySystemNotificationRepository.IsExisting(x => x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var updateEntity = _mapper.Map<SystemNotification>(entity);

        _updateSystemNotificationRepository.Update(updateEntity);
    }

    public async Task DeleteAsync(int id)
    {
        bool exists = await _querySystemNotificationRepository.IsExisting(x => x.Id == id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteSystemNotificationRepository.RemoveById(id);
    }
}
