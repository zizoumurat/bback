using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.NotificationRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class NotificationService : INotificationService
{
    private readonly IAddNotificationRepository _addNotificationRepository;
    private readonly IUpdateNotificationRepository _updateNotificationRepository;
    private readonly IDeleteNotificationRepository _deleteNotificationRepository;
    private readonly IQueryNotificationRepository _queryNotificationRepository;
    private readonly ILocalizationService _localizationService;
    private readonly ISendNotificationService _sendNotificationService;
    private readonly IMapper _mapper;

    public NotificationService(IAddNotificationRepository addNotificationRepository,
        IUpdateNotificationRepository updateNotificationRepository,
        IDeleteNotificationRepository deleteNotificationRepository,
        IQueryNotificationRepository queryNotificationRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        ISendNotificationService sendNotificationService)
    {
        _addNotificationRepository = addNotificationRepository;
        _updateNotificationRepository = updateNotificationRepository;
        _deleteNotificationRepository = deleteNotificationRepository;
        _queryNotificationRepository = queryNotificationRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _sendNotificationService = sendNotificationService;
    }

    public async Task AddAsync(NotificationDto entity)
    {
        var addEntity = _mapper.Map<Notification>(entity);

        await _addNotificationRepository.AddAsync(addEntity);

        _ = Task.Run(() => _sendNotificationService.SendNotificationToUserAsync(entity.UserId.ToString(), entity.Message));
    }

    public async Task DeleteAsync(int id)
    {
        bool exists = await _queryNotificationRepository.IsExisting(x => x.Id == id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteNotificationRepository.RemoveById(id);
    }

    public async Task<IList<NotificationDto>> GetAllWithOutPaginationAsync(int userId)
    {
        var list = await _queryNotificationRepository.GetList(x => x.UserId == userId).ToListAsync();

        return _mapper.Map<List<NotificationDto>>(list);
    }
}
