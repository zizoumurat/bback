using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Repositories.SupplierActionRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class SupplierActionService : ISupplierActionService
{
    private readonly IAddSupplierActionRepository _addSupplierActionRepository;
    private readonly IUpdateSupplierActionRepository _updateSupplierActionRepository;
    private readonly IQuerySupplierActionRepository _querySupplierActionRepository;
    private readonly ILocalizationService _localizationService;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public SupplierActionService(IAddSupplierActionRepository addSupplierActionRepository, ILocalizationService localizationService, IMapper mapper, IQuerySupplierActionRepository querySupplierActionRepository, IUpdateSupplierActionRepository updateSupplierActionRepository, INotificationService notificationService)
    {
        _addSupplierActionRepository = addSupplierActionRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _querySupplierActionRepository = querySupplierActionRepository;
        _updateSupplierActionRepository = updateSupplierActionRepository;
        _notificationService = notificationService;
    }

    public async Task AddAsync(int companyId, int userId, SupplierActionCreateDto entity)
    {

        var addEntity = _mapper.Map<SupplierAction>(entity);
        addEntity.CompanyId = companyId;
        addEntity.UserId = userId;
        addEntity.SupplierActionStatus = SupplierActionStatusEnum.Pending;

        await _addSupplierActionRepository.AddAsync(addEntity);
    }

    public async Task UpdateStatusAsync(int supplierId, SupplierActionUpdateStatusDto entity)
    {
        var existEntity = await _querySupplierActionRepository.GetFirstAsync(x => x.Id == entity.Id)
                .Include(x => x.Supplier).ThenInclude(x => x.Company).FirstAsync();

        if (existEntity == null || existEntity.SupplierId != supplierId)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("EntityNotFound"));
        }

        existEntity.SupplierNotes = entity.SupplierNotes;
        existEntity.SupplierActionStatus = entity.SupplierActionStatus;

        _updateSupplierActionRepository.Update(existEntity);

        string message = $"{existEntity.Supplier.Company.Name} Firmanızın Tanımlamış Olduğu Düzeltici / Önleyici Aksiyonun Statüsünü Güncelledi.";

        var notificationDto = new NotificationDto(0, existEntity.UserId, message, false);

        await _notificationService.AddAsync(notificationDto);
    }

    public async Task<IList<SupplierActionListDto>> GetListAsync(int companyId, int supplierId)
    {
        var result = await _querySupplierActionRepository.GetList(x => x.CompanyId == companyId && x.SupplierId == supplierId)
            .ProjectTo<SupplierActionListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(); ;

        return result;
    }

}
