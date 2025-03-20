using AutoMapper;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.ApprovalChainRepositories;
using Buyersoft.Domain.Repositories.BudgetRepositories;
using Buyersoft.Domain.Repositories.CategoryRepositories;
using Buyersoft.Domain.Repositories.OfferRepositories;
using Buyersoft.Domain.Repositories.RequestGroupRepositories;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class RequestService : IRequestService
{
    private readonly IAddRequestRepository _addRequestRepository;
    private readonly IUpdateRequestRepository _updateRequestRepository;
    private readonly IDeleteRequestRepository _deleteRequestRepository;
    private readonly IQueryRequestRepository _queryRequestRepository;
    private readonly IQueryRequestGroupRepository _queryRequestGroupRepository;
    private readonly IQueryCategoryRepository _queryCategoryRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IMapper _mapper;
    private readonly IUpdateBudgetRepository _updateBudgetRepository;
    private readonly IQueryBudgetRepository _queryBudgetRepository;
    private readonly IAddOfferRepository _addOfferRepository;
    private readonly ISendNotificationService _sendNotificationService;
    private readonly IAuthService _authService;
    private readonly IQueryApprovalChainRepository _queryApprovalChainRepository;
    private readonly INotificationService _notificationService;
    private readonly UserManager<User> _userManager;

    public RequestService(IAddRequestRepository addRequestRepository,
        IUpdateRequestRepository updateRequestRepository,
        IDeleteRequestRepository deleteRequestRepository,
        IQueryRequestRepository queryRequestRepository,
        ILocalizationService localizationService,
        IMapper mapper,
        IQueryRequestGroupRepository queryRequestGroupRepository,
        IQueryCategoryRepository queryCategoryRepository,
        IUpdateBudgetRepository updateBudgetRepository,
        IQueryBudgetRepository queryBudgetRepository,
        IAddOfferRepository addOfferRepository,
        ISendNotificationService sendNotificationService,
        IAuthService authService,
        IQueryApprovalChainRepository queryApprovalChainRepository,
        INotificationService notificationService,
        UserManager<User> userManager)
    {
        _addRequestRepository = addRequestRepository;
        _updateRequestRepository = updateRequestRepository;
        _deleteRequestRepository = deleteRequestRepository;
        _queryRequestRepository = queryRequestRepository;
        _localizationService = localizationService;
        _mapper = mapper;
        _queryRequestGroupRepository = queryRequestGroupRepository;
        _queryCategoryRepository = queryCategoryRepository;
        _updateBudgetRepository = updateBudgetRepository;
        _queryBudgetRepository = queryBudgetRepository;
        _addOfferRepository = addOfferRepository;
        _sendNotificationService = sendNotificationService;
        _authService = authService;
        _queryApprovalChainRepository = queryApprovalChainRepository;
        _notificationService = notificationService;
        _userManager = userManager;
    }

    public async Task<PaginatedList<RequestListDto>> GetAllAsync(int companyId, int userId, RequestFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var isRevised = filter.IsRevised ?? false;

        var query = _queryRequestRepository
            .GetList(x =>
                x.CompanyId == companyId ||
                x.Offers.Any(o =>
                    o.CompanyId == companyId &&
                    o.OfferStatus == OfferStatus.PendingOffer &&
                    o.IsRevised == isRevised &&
                    o.Request.BidCollectionEndDate > DateTime.UtcNow
                )
            )
            .Include(x => x.Template)
            .Include(x => x.Category)
                .ThenInclude(c => c.CategoryUsers)
            .AsQueryable();


        if (filter != null)
        {
            query = query.Where(x => filter.CompanyId == default || x.CompanyId == filter.CompanyId)
                .Where(x => filter.IsReverseAuction == default || x.ReverseAuctionId != null)
                .Where(x => filter.CurrencyId == default || x.CurrencyId == filter.CurrencyId)
                .Where(x => filter.CategoryId == default || x.CategoryId == filter.CategoryId)
                .Where(x => filter.TemplateId == default || x.TemplateId == filter.TemplateId)
                .Where(x => filter.CreatedById == default || x.CreatedById == filter.CreatedById)
                .Where(x => filter.ManagerId == default || x.ManagerId == filter.ManagerId)
                .Where(x => filter.BudgetId == default || x.BudgetId == filter.BudgetId)
                .Where(x => filter.IsApproved == default || filter.State == default || x.State == filter.State)
                .Where(x => filter.RequestCode == default || x.RequestCode == filter.RequestCode)
                .Where(x => filter.Title == default || x.Title.ToLower().Contains(filter.Title.ToLower()))
                .Where(x => filter.Reason == default || x.Reason == filter.Reason)
                .Where(x => filter.CollectionChannel == default || x.CollectionChannel == filter.CollectionChannel)
                .Where(x => filter.LocationId == default || x.Category.LocationId == filter.LocationId)
                .Where(x => filter.BudgetInclusionStatus == default || x.BudgetInclusionStatus == filter.BudgetInclusionStatus)
                .Where(x => filter.RequestedSupplyDate == default || x.RequestedSupplyDate == filter.RequestedSupplyDate)
                .Where(x => filter.IsForApproval == default || x.Approvals.Any(x => x.UserId == userId && x.Status == ApprovalStatus.Pending))
                .Where(x => filter.IsForApprovalArchive == default || x.Approvals.Any(x => x.UserId == userId))
                .Where(x => filter.EstimatedSupplyDate == default || x.EstimatedSupplyDate == filter.EstimatedSupplyDate);
        }

        if (pagination.sortByMultiName.First() == "createdBy")
        {
            pagination.sortByMultiName[0] = "createdBy.name";
        }
        if (pagination.sortByMultiName.First() == "locationName")
        {
            pagination.sortByMultiName[0] = "category.location.name";
        }
        if (pagination.sortByMultiName.First() == "currencyName")
        {
            pagination.sortByMultiName[0] = "currency.name";
        }
        if (pagination.sortByMultiName.First() == "companyName")
        {
            pagination.sortByMultiName[0] = "company.name";
        }
        if (pagination.sortByMultiName.First() == "manager")
        {
            pagination.sortByMultiName[0] = "manager.name";
        }

        if (pagination.sortByMultiName.First() == "ownerUserList")
            query = query.OrderBy(x => x.Category.CategoryUsers.First().User.Name);

        else
            query = query.MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder);

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new RequestListDto()
            {
                Id = x.Id,
                CompanyName = x.Company.Name,
                Title = x.Title,
                RequestCode = x.RequestCode ?? "",
                Amount = x.Amount,
                RequestedSupplyDate = x.RequestedSupplyDate,
                EstimatedSupplyDate = x.EstimatedSupplyDate,
                CollectionChannel = x.CollectionChannel.Value,
                Reason = x.Reason,
                CurrencyId = x.CurrencyId,
                CurrencyName = x.Currency.Name,
                TemplateId = x.TemplateId,
                CreatedById = x.CreatedById,
                CreatedBy = x.CreatedBy != null ? $"{x.CreatedBy.Name} {x.CreatedBy.Surname}" : "",
                ManagerId = x.ManagerId,
                Manager = x.Manager != null ? $"{x.Manager.Name} {x.Manager.Surname}" : "",
                OwnerUserList = x.Manager != null
                    ? new List<string> { x.Manager.Name + " " + x.Manager.Surname }
                    : x.Category.CategoryUsers.Select(user => user.User.Name + " " + user.User.Surname).ToList(),
                LocationName = x.Category.Location.Name ?? "",
                State = x.State,
                ApprovedDate = x.ApprovedDate,
                BudgetId = x.BudgetId,
                BudgetInclusionStatus = x.BudgetInclusionStatus,
                CategoryUsers = x.Category.CategoryUsers.Select(x => x.UserId).ToArray(),
                Template = _mapper.Map<TemplateDto>(x.Template)

            }).ToListAsync();


        return new PaginatedList<RequestListDto>(items, count, pagination.Page, pagination.PageSize);
    }
    public async Task AddAsync(int companyId, int userId, CreateRequestDto entity, string userName)
    {
        var category = await _queryCategoryRepository.GetList(x => x.CompanyId == companyId && x.Id == entity.CategoryId && !x.IsDeleted)
              .Include(x => x.CategoryUsers).ToListAsync();

        if (category == null || category.Count == 0)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("CategoryNotFound"));
        }

        var users = category.First().CategoryUsers;

        if (users.Count == 0)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("CategoryUserNotFound"));
        }

        var addEntity = _mapper.Map<Request>(entity);
        addEntity.CompanyId = companyId;
        addEntity.CreatedById = userId;
        addEntity.ManagerId = users.Count == 1 ? users.First().UserId : null;
        addEntity.State = RequestStateEnum.NotStarted;
        addEntity.CancellationReason = "";

        var count = _queryRequestRepository.GetList(x => x.CompanyId == companyId).Count() + 1;
        count++;

        int hashCode = (count.ToString() + count.ToString()).GetHashCode();
        addEntity.RequestCode = (Math.Abs(hashCode) % 90000 + 10000).ToString();

        await _addRequestRepository.AddAsync(addEntity);

        foreach (var user in users)
        {
            var findedUser = await _authService.GetById(userId);
            string message = $"{findedUser.Name} {findedUser.Surname} Kullanıcısı Sizin İçin Yeni Bir Talep Oluşturdu";

            var notificationDto = new NotificationDto(0, user.UserId, message, false);

            await _notificationService.AddAsync(notificationDto);
        }


        if (entity.BudgetId == null)
        {
            return;
        }

        var budget = await _queryBudgetRepository.GetFirstAsync(x => x.CompanyId == companyId && x.Id == entity.BudgetId).FirstAsync()
            ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("BudgetNotFound"));

        if (budget.AvailableLimit < addEntity.Amount)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("AmountNotAvailable"));
        }

        budget.AvailableLimit -= entity.Amount;

        _updateBudgetRepository.Update(budget);
    }
    public async Task UpdateAsync(int companyId, CreateRequestDto entity)
    {
        bool exists = false;

        if (exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("DuplicateEntity"));
        }

        exists = await _queryRequestRepository.IsExisting(x => x.CompanyId == companyId && x.Id == entity.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var existingEntity = await _queryRequestRepository.GetFirstAsync(x => x.Id == entity.Id).FirstAsync();

        var updateEntity = _mapper.Map<Request>(entity);

        updateEntity.CompanyId = companyId;
        updateEntity.CreatedById = existingEntity.CreatedById;
        updateEntity.ManagerId = existingEntity.ManagerId;
        updateEntity.State = existingEntity.State;
        updateEntity.RequestCode = existingEntity.RequestCode;


        _updateRequestRepository.Update(updateEntity);

    }
    public async Task DeleteAsync(int id, int companyId)
    {
        bool exists = await _queryRequestRepository.IsExisting(x => x.Id == id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        _deleteRequestRepository.RemoveById(id);
    }
    public async Task<RequestListDto> GetById(int companyId, int Id)
    {
        bool exists = await _queryRequestRepository.IsExisting(x => x.Id == Id
        && (x.CompanyId == companyId || x.Offers.Any(x => x.CompanyId == companyId)));

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var request = await _queryRequestRepository.GetList(x => x.Id == Id)
            .Include(x => x.Template)
            .Include(x => x.Manager)
            .Include(x => x.CreatedBy)
            .Include(x => x.Currency)
            .Include(x => x.Category)
                .ThenInclude(x => x.SubCategory)
                    .ThenInclude(x => x.CompanySubCategory)
            .Include(x => x.Category)
                .ThenInclude(x => x.RequestGroup)
                    .ThenInclude(x => x.CompanyRequestGroup)
            .Include(x => x.Category)
                .ThenInclude(x => x.Location)
           .Include(x => x.Budget)
                .ThenInclude(x => x.Currency)
           .FirstAsync();


        return _mapper.Map<RequestListDto>(request);
    }
    public async Task StartBidCollection(StartBidCollectionDto model)
    {
        bool exists = await _queryRequestRepository.IsExisting(x => x.Id == model.Request.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var existingEntity = await _queryRequestRepository
            .GetFirstAsync(x => x.Id == model.Request.Id, true)
            .Include(x => x.Company)
            .Include(x => x.Offers).FirstAsync();
        int companyId = existingEntity.CompanyId;
        int createdById = existingEntity.CreatedById;
        int? managerId = existingEntity.ManagerId;

        _mapper.Map(model.Request, existingEntity);
        existingEntity.BidCollectionEndDate = DateTime.Now.AddDays(3);
        existingEntity.State = RequestStateEnum.Started;
        existingEntity.CompanyId = companyId;
        existingEntity.CreatedById = createdById;
        existingEntity.ManagerId = managerId;
        existingEntity.CancellationReason = string.Empty;

        var currentDate = DateTime.UtcNow.ToString("mmss");
        var offerList = model.ProviderIdList
            .Select((providerId, index) => new Offer
            {
                RequestId = model.Request.Id,
                CompanyId = providerId,
                OfferStatus = OfferStatus.PendingOffer,
                TotalPrice = 0,
                MaturityDays = 0,
                AverageUnitPrice = 0,
                ReferenceCode = $"{model.Request.Id}{providerId}{index + 1}{currentDate}",
                RejectionReason = string.Empty,
            })
            .ToList();

        existingEntity.Offers.Clear();
        existingEntity.Offers = offerList;

        var users = await _userManager.Users.Where(x => model.ProviderIdList.Contains(x.CompanyId) && x.RoleId == 3).ToListAsync();

        foreach (var user in users)
        {
            string message = $"{existingEntity.Company.Name} Firması Sizden Teklif Talebinde Bulundu";

            var notificationDto = new NotificationDto(0, user.Id, message, false);

            await _notificationService.AddAsync(notificationDto);
        }

        _updateRequestRepository.Update(existingEntity);

    }
    public async Task CancelBidCollection(CancelBidCollectionDto model)
    {
        bool exists = await _queryRequestRepository.IsExisting(x => x.Id == model.Id);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var request = await _queryRequestRepository.GetFirstAsync(x => x.Id == model.Id).FirstAsync();
        request.State = RequestStateEnum.Cancelled;
        request.CancellationReason = model.CancellationReasion;

        _updateRequestRepository.Update(request);
    }
    public async Task AssignManager(int companyId, int userId, AssignManagerDto model)
    {
        bool exists = await _queryRequestRepository.IsExisting(x => x.Id == model.Id && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var request = await _queryRequestRepository.GetFirstAsync(x => x.Id == model.Id)
                .Include(x => x.Category)
                .ThenInclude(c => c.CategoryUsers).FirstAsync();

        if (request.ManagerId != null)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("ManagerAlreadyAssigned"));
        }

        if (request.Category.CategoryUsers.All(x => x.UserId != userId))
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("UserNotInCategory"));
        }

        request.ManagerId = userId;

        _updateRequestRepository.Update(request);
    }
    public async Task CreateComprasionTable(int companyId, int requestId, int offerType)
    {
        bool exists = await _queryRequestRepository.IsExisting(x => x.Id == requestId && x.CompanyId == companyId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var request = await _queryRequestRepository.GetFirstAsync(x => x.Id == requestId, true)
            .Include(x => x.Offers)
                .ThenInclude(x => x.OfferDetails).FirstAsync();

        request.State = RequestStateEnum.ComparisonTableCreated;

        foreach (var item in request.Offers)
        {
            foreach (var detail in item.OfferDetails)
            {
                detail.Allocation = null;
            }

            if (offerType == 1 || item.AddedToShortList)
            {

                item.AddedToComparisonTable = true;
            }
        }

        _updateRequestRepository.Update(request);
    }

    public async Task StartApprovalProcess(int companyId, StartApprovalProcessDto model)
    {
        var request = await _queryRequestRepository
        .GetFirstAsync(x => x.Id == model.Id, true).Include(x => x.Approvals).FirstAsync() ??
        throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));

        if (request.State >= RequestStateEnum.PendingApprovals)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("ApprovalProcessAlreadyStarted"));
        }

        request.State = RequestStateEnum.PendingApprovals;
        request.TechnicalEvaluation = model.TechnicalEvaluation;
        request.CommercialEvaluation = model.CommercialEvaluation;


        // Onay zincirlerini, request.Amount'a uygun olanları almak için filtreliyoruz.
        var relevantApprovalChains = await _queryApprovalChainRepository
            .GetList(x => x.CompanyId == companyId && x.SpendLimit >= request.Amount)
            .Include(x => x.ApprovalChainUsers)
            .OrderBy(x => x.SpendLimit)
            .ToListAsync();

        // Eğer hiç uygun onay zinciri bulunmazsa hata fırlatıyoruz.
        if (!relevantApprovalChains.Any())
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NoApprovalChainsFound"));
        }

        request.Approvals.Clear();

        // Tüm onaylayıcıları bir set içinde tutarak UserId'ye göre tekilleştiriyoruz.
        var uniqueUsers = new HashSet<int>();

        foreach (var approvalChain in relevantApprovalChains)
        {
            foreach (var item in approvalChain.ApprovalChainUsers)
            {
                // Eğer bu UserId daha önce eklenmemişse, onu ekliyoruz.
                if (uniqueUsers.Add(item.UserId))
                {
                    request.Approvals.Add(new Approval()
                    {
                        RequestId = model.Id,
                        Status = ApprovalStatus.Pending,
                        UserId = item.UserId,
                        Comment = string.Empty
                    });
                }
            }
        }

        _updateRequestRepository.Update(request);

    }

    public async Task ApproveRejectRequest(int userId, ApproveRejectRequestDto model)
    {
        var request = await _queryRequestRepository
            .GetFirstAsync(x => x.Id == model.Id && x.Approvals.Any(a => a.UserId == userId), true)
                .Include(x => x.Offers.Where(xx => xx.IsSelected))
                .Include(x => x.Approvals).FirstAsync() ??
                throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));

        var approval = request.Approvals.First(x => x.UserId == userId);

        if (approval.Status != ApprovalStatus.Pending)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("ApprovalAlreadyDone"));
        }

        //aproval sending response

        approval.Status = model.Status;
        approval.Comment = model.Comment;

        if (!request.Approvals.Any(x => x.Status == ApprovalStatus.Pending || x.Status == ApprovalStatus.Rejected))
        {
            request.Contracts = new List<Contract>();
            request.State = request.State = RequestStateEnum.Approved;

            foreach (var item in request.Offers.Where(x => x.IsSelected))
            {
                var contract = new Contract
                {
                    CompanyId = item.CompanyId,
                    RequestId = request.Id,
                    OfferId = item.Id,
                    TotalPrice = item.TotalPrice,
                    ContractStatus = ContractStatus.NotStarted,
                    ReferenceCode = item.ReferenceCode,
                    StartDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddDays(30)
                };

                request.Contracts.Add(contract);
            }
        }

        if (model.Status == ApprovalStatus.Rejected)
        {
            request.State = RequestStateEnum.Rejected;
        }

        _updateRequestRepository.Update(request);
    }
}
