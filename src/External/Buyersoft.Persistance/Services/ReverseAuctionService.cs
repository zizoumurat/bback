using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Buyersoft.Domain.Repositories.ReverseAuctionRepositories;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;

public class ReverseAuctionService : IReverseAuctionService
{
    private readonly IQueryRequestRepository _queryRequestRepository;
    private readonly IUpdateRequestRepository _updateRequestRepository;
    private readonly IAddReverseAuctionRepository _addReverseAuctionRepository;
    private readonly IUpdateReverseAuctionRepository _updateReverseAuctionRepository;
    private readonly IQueryReverseAuctionRepository _queryReverseAuctionRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly ISendNotificationService _sendNotificationService;

    public ReverseAuctionService(IAddReverseAuctionRepository addReverseAuctionRepository,
        IUpdateReverseAuctionRepository updateReverseAuctionRepository,
        IQueryReverseAuctionRepository queryReverseAuctionRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IQueryRequestRepository queryRequestRepository,
        IUpdateRequestRepository updateRequestRepository,
        ISendNotificationService sendNotificationService)
    {
        _addReverseAuctionRepository = addReverseAuctionRepository;
        _updateReverseAuctionRepository = updateReverseAuctionRepository;
        _queryReverseAuctionRepository = queryReverseAuctionRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _queryRequestRepository = queryRequestRepository;
        _updateRequestRepository = updateRequestRepository;
        _sendNotificationService = sendNotificationService;
    }

    public async Task<PaginatedList<ReverseAuctionListDto>> GetAllAsync(int companyId, ReverseAuctionFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _queryReverseAuctionRepository.GetList(x => x.Request.CompanyId == companyId);

        if (filter != null)
        {
            query = query.Where(x =>
                (filter.StartTime == default || x.StartTime >= filter.StartTime) &&
                (filter.EndTime == default || x.EndTime <= filter.EndTime) &&
                (filter.RequestId == default || x.RequestId == filter.RequestId)
            );
        }

        query = query
            .Include(x => x.Request)
            .ThenInclude(x => x.Offers)
            .ThenInclude(x => x.Company).AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<ReverseAuctionListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<ReverseAuctionListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, AddReverseAuctionDto model)
    {
        bool exists = await _queryReverseAuctionRepository.IsExisting(x => x.RequestId == model.RequestId);

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("ReverseAuctionAlreadyCreated"));
        }

        var addEntity = _mapper.Map<ReverseAuction>(model);

        addEntity.Statu = ReverseAuctionStatusEnum.NotStarted;
        var timeDifference = model.EndTime - model.StartTime;
        addEntity.Minutes = (int)timeDifference.TotalMinutes;

        await _addReverseAuctionRepository.AddAsync(addEntity);

        var request = await _queryRequestRepository
               .GetFirstAsync(x => x.CompanyId == companyId && x.Id == model.RequestId, true)
               .Include(x => x.Offers)
               .FirstAsync()
                ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("RequestNotFound"));

        request.State = RequestStateEnum.ReverseAuctionPending;
        request.ReverseAuctionId = addEntity.Id;

        var offers = request.Offers.Where(x => model.OfferIdList.Contains(x.Id));

        foreach (var offer in offers)
        {
            offer.AddedToReverseAuction = true;
        }

        _updateRequestRepository.Update(request);
    }

    public async Task<ReverseAuctionListDto> GetById(int Id)
    {

        bool exists = await _queryReverseAuctionRepository.IsExisting(x => x.Id == Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var template = await _queryReverseAuctionRepository.GetByIdAsync(Id);

        return _mapper.Map<ReverseAuctionListDto>(template);
    }

    public async Task ChangeStatu(int id, ReverseAuctionStatusEnum statu, int remainingSeconds)
    {
        bool exists = await _queryReverseAuctionRepository.IsExisting(x => x.Id == id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var entity = await _queryReverseAuctionRepository.GetByIdAsync(id, true);

        entity.Statu = statu;
        entity.Minutes = remainingSeconds / 60;

        _updateReverseAuctionRepository.Update(entity);
        _ = _sendNotificationService.ChangeStatu(entity.RequestId.ToString(), statu, remainingSeconds);
    }
}
