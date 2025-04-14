using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.OfferRepositories;
using Buyersoft.Domain.Repositories.RequestRepositories;
using Buyersoft.Domain.Repositories.TemplateRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

namespace Buyersoft.Persistance.Services;
public class OfferService : IOfferService
{
    private readonly IAddOfferRepository _addOfferRepository;
    private readonly IQueryRequestRepository _queryRequestRepository;
    private readonly IQueryOfferRepository _queryOfferRepository;
    private readonly IUpdateRequestRepository _updateRequestRepository;
    private readonly IUpdateOfferRepository _updateOfferRepository;
    private readonly ILocalizationService _localizationService;
    private readonly ITemplateService _templateService;
    private readonly IQueryTemplateRepository _queryTemplateRepository;
    private readonly IDocumentService _documentService;
    private readonly IMapper _mapper;
    private readonly ISendNotificationService _sendNotificationService;

    public OfferService(IUpdateOfferRepository updateOfferRepository, IQueryOfferRepository queryOfferRepository, ILocalizationService localizationService, ITemplateService templateService, IQueryTemplateRepository queryTemplateRepository, IMapper mapper, IDocumentService documentService, IQueryRequestRepository queryRequestRepository, IUpdateRequestRepository updateRequestRepository, IAddOfferRepository addOfferRepository, ISendNotificationService sendNotificationService)
    {
        _updateOfferRepository = updateOfferRepository;
        _queryOfferRepository = queryOfferRepository;
        _localizationService = localizationService;
        _templateService = templateService;
        _queryTemplateRepository = queryTemplateRepository;
        _mapper = mapper;
        _documentService = documentService;
        _queryRequestRepository = queryRequestRepository;
        _updateRequestRepository = updateRequestRepository;
        _addOfferRepository = addOfferRepository;
        _sendNotificationService = sendNotificationService;
    }

    public async Task<PaginatedList<RequestListDto>> GetAllAsync(int companyId, RequestFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _queryOfferRepository.GetList(x =>
        (x.Request.CompanyId == companyId || x.Request.Offers.Any(x => x.CompanyId == companyId)) &&
        x.OfferStatus == OfferStatus.OfferSubmitted)
            .Include(x => x.Request)
                .ThenInclude(x => x.Template)
            .Include(x => x.Request)
            .AsQueryable();

        var offerList = _queryOfferRepository.GetList(x => x.Contract != null && x.Contract.ContractStatus == ContractStatus.ContractApproved)
            .Include(x => x.Request)
                .ThenInclude(x => x.Template)
            .Include(x => x.OfferDetails);



        if (filter != null)
        {
            query = query.Where(x => filter.CurrencyId == default || x.Request.CurrencyId == filter.CurrencyId)
                .Where(x => filter.CategoryId == default || x.Request.CategoryId == filter.CategoryId)
                .Where(x => filter.TemplateId == default || x.Request.TemplateId == filter.TemplateId)
                .Where(x => filter.CreatedById == default || x.Request.CreatedById == filter.CreatedById)
                .Where(x => filter.ManagerId == default || x.Request.ManagerId == filter.ManagerId)
                .Where(x => filter.BudgetId == default || x.Request.BudgetId == filter.BudgetId)
                .Where(x => filter.IsApproved == default || filter.State == default || x.Request.State == filter.State)
                .Where(x => filter.RequestCode == default || x.Request.RequestCode == filter.RequestCode)
                .Where(x => filter.Title == default || x.Request.Title.ToLower().Contains(filter.Title.ToLower()))
                .Where(x => filter.Reason == default || x.Request.Reason == filter.Reason)
                .Where(x => filter.CollectionChannel == default || x.Request.CollectionChannel == filter.CollectionChannel)
                .Where(x => filter.LocationId == default || x.Request.Category.LocationId == filter.LocationId)
                .Where(x => filter.BudgetInclusionStatus == default || x.Request.BudgetInclusionStatus == filter.BudgetInclusionStatus)
                .Where(x => filter.RequestedSupplyDate == default || x.Request.RequestedSupplyDate == filter.RequestedSupplyDate)
                .Where(x => filter.EstimatedSupplyDate == default || x.Request.EstimatedSupplyDate == filter.EstimatedSupplyDate);
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
            query = query.OrderBy(x => x.Request.Category.CategoryUsers.First().User.Name);

        else
            query = query.MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder);

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new RequestListDto()
            {
                Id = x.Request.Id,
                OfferId = x.Id,
                CompanyName = x.Request.Company.Name,
                Title = x.Request.Title,
                RequestCode = x.Request.RequestCode ?? "",
                Amount = x.TotalPrice,
                RequestedSupplyDate = x.Request.RequestedSupplyDate,
                EstimatedSupplyDate = x.Request.EstimatedSupplyDate,
                CollectionChannel = x.Request.CollectionChannel.Value,
                Reason = x.Request.Reason,
                CurrencyId = x.Request.CurrencyId,
                CurrencyName = x.Request.Currency.Name,
                TemplateId = x.Request.TemplateId,
                CreatedById = x.Request.CreatedById,
                CreatedBy = x.Request.CreatedBy != null ? $"{x.Request.CreatedBy.Name} {x.Request.CreatedBy.Surname}" : "",
                ManagerId = x.Request.ManagerId,
                Manager = x.Request.Manager != null ? $"{x.Request.Manager.Name} {x.Request.Manager.Surname}" : "",
                OwnerUserList = x.Request.Manager != null
                    ? new List<string> { x.Request.Manager.Name + " " + x.Request.Manager.Surname }
                    : x.Request.Category.CategoryUsers.Select(user => user.User.Name + " " + user.User.Surname).ToList(),
                LocationName = x.Request.Category.Location.Name ?? "",
                State = x.Request.State,
                ApprovedDate = x.Request.ApprovedDate,
                BudgetId = x.Request.BudgetId,
                BudgetInclusionStatus = x.Request.BudgetInclusionStatus,
                CategoryUsers = x.Request.Category.CategoryUsers.Select(x => x.UserId).ToArray(),
                Template = _mapper.Map<TemplateDto>(x.Request.Template),
                OfferCode = x.ReferenceCode

            }).ToListAsync();


        return new PaginatedList<RequestListDto>(items, count, pagination.Page, pagination.PageSize);
    }


    public async Task MakeOfferAsync(int companyId, MakeOfferDto model)
    {
        bool exists = await _queryOfferRepository.IsExisting(x => x.CompanyId == companyId && x.RequestId == model.RequestId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }

        var offer = await _queryOfferRepository
            .GetFirstAsync(x => x.CompanyId == companyId && x.RequestId == model.RequestId && x.OfferStatus == OfferStatus.PendingOffer)
            .Include(x => x.Request).FirstAsync() ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidOperation"));

        if (offer.Request.BidCollectionEndDate < DateTime.UtcNow)
            throw new InvalidOperationException(_localizationService.GetLocalizedString("BidCollectionProcessEnded"));

        offer.OfferStatus = OfferStatus.OfferSubmitted;
        offer.MaturityDays = model.MaturityDays;
        offer.Notes = model.Notes;
        offer.OfferDate = DateTime.Now;
        offer.ExpirationDate = model.ExpirationDate;

        var count = _queryOfferRepository.GetList(x => x.RequestId == model.RequestId).Count() + 1;

        var template = await _queryTemplateRepository.GetByIdAsync(offer.Request.TemplateId);

        if (template == default)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidOperation"));
        }

        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(template.Data);

        var rows = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(data["rows"].ToString());


        if (model.PriceList.Count != rows.Count)
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidOperation"));

        decimal totalAmount = 0;
        var totalQuantity = 0;

        var offerDetail = new List<OfferDetail>();

        for (int i = 0; i < model.PriceList.Count; i++)
        {
            int quantity = Convert.ToInt32(rows[i]["quantity"].ToString());
            totalQuantity += quantity;
            totalAmount += model.PriceList[i] * quantity;

            offerDetail.Add(new OfferDetail()
            {
                OfferId = offer.Id,
                Quantity = quantity,
                TotalPrice = model.PriceList[i] * quantity,
                UnitPrice = model.PriceList[i],
                FirstUnitPrice = model.PriceList[i],
                ProductDefinition = rows[i]["productDefinition"].ToString()
            });
        }

        offer.OfferDetails = offerDetail;
        offer.TotalPrice = totalAmount;
        offer.AverageUnitPrice = totalAmount / totalQuantity;
        offer.OfferStatus = OfferStatus.OfferSubmitted;

        if (model.Document != null)
        {
            int fileId = await _documentService.UploadDocument(model.Document);
            offer.DocumentId = fileId;
        }

        _updateOfferRepository.Update(offer);
    }

    public async Task RejectOfferAsync(int companyId, RejectOfferDto model)
    {
        bool exists = await _queryOfferRepository.IsExisting(x => x.CompanyId == companyId && x.RequestId == model.RequestId);

        if (!exists)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));
        }


        var offer = await _queryOfferRepository
            .GetFirstAsync(x => x.CompanyId == companyId && x.RequestId == model.RequestId && x.OfferStatus == OfferStatus.PendingOffer)
            .Include(x => x.Request)
            .FirstAsync();

        offer.OfferStatus = OfferStatus.OfferRequestRejected;
        offer.RejectionReason = model.RejectionReason;

        _updateOfferRepository.Update(offer);
    }

    public async Task<List<OfferListDto>> GetOfferListByRequest(int companyId, int requestId)
    {
        var offers = await _queryOfferRepository.GetList(x =>
                (x.Request.CompanyId == companyId || x.Request.Offers.Any(x => x.CompanyId == companyId)) &&
                x.RequestId == requestId &&
                x.OfferStatus == OfferStatus.OfferSubmitted)
            .Include(x => x.Company)
            .Include(x => x.OfferDetails)
            .Include(x => x.Document)
            .ToListAsync();

        // Gruplama ve son revizyonu seçme işlemini bellek üzerinde yap
        var groupedOffers = offers
            .GroupBy(x => x.OriginalOfferId ?? x.Id) // Orijinal teklif veya kendisi
            .Select(group => group.OrderByDescending(x => x.Id).First()) // Son revizyon veya orijinal teklif
            .ToList();

        // DTO'ya projeksiyon
        var result = groupedOffers
            .Select(offer => _mapper.Map<OfferListDto>(offer))
            .ToList();

        return result;
    }

    public async Task AddToShortList(int companyId, int offerId)
    {
        var offer = await _queryOfferRepository.GetFirstAsync(x => x.Request.CompanyId == companyId && x.Id == offerId).FirstAsync()
            ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));

        offer.AddedToShortList = true;

        _updateOfferRepository.Update(offer);
    }

    public async Task RemoveToShortList(int companyId, int offerId)
    {
        var offer = await _queryOfferRepository.GetFirstAsync(x => x.Request.CompanyId == companyId && x.Id == offerId).FirstAsync()
            ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));

        offer.AddedToShortList = false;

        _updateOfferRepository.Update(offer);
    }

    public async Task AddToFavorite(int companyId, int offerId)
    {
        var offer = await _queryOfferRepository.GetFirstAsync(x => x.Request.CompanyId == companyId && x.Id == offerId).FirstAsync()
            ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));

        offer.IsSelected = true;

        _updateOfferRepository.Update(offer);
    }

    public async Task RemoveToFavorite(int companyId, int offerId)
    {
        var offer = await _queryOfferRepository.GetFirstAsync(x => x.Request.CompanyId == companyId && x.Id == offerId).FirstAsync()
            ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));

        offer.IsSelected = false;

        _updateOfferRepository.Update(offer);
    }

    public async Task SetAllocation(int companyId, int RequestId, List<OfferDetailDto> offerDetailList)
    {

        var request = await _queryRequestRepository.GetFirstAsync(x => x.Id == RequestId && x.CompanyId == companyId, true)
            .Include(x => x.Template)
            .Include(x => x.Offers)
                .ThenInclude(x => x.OfferDetails)
             .FirstOrDefaultAsync();

        if (request.State > RequestStateEnum.AllocationCreated)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidOperation"));
        }


        if (request == default)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidOperation"));
        }


        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(request.Template.Data);
        var rows = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(data["rows"].ToString());

        foreach (var offer in request.Offers)
        {
            offer.IsSelected = false;

        }

        foreach (var offerDetail in offerDetailList)
        {
            var offerDetailEntity = request.Offers.FirstOrDefault(x => x.Id == offerDetail.OfferId)?.OfferDetails.FirstOrDefault(x => x.Id == offerDetail.Id);

            if (offerDetailEntity == default)
                throw new InvalidOperationException(_localizationService.GetLocalizedString("InvalidOperation"));


            offerDetailEntity.Allocation = offerDetail.Allocation;
            offerDetailEntity.TotalPrice = offerDetailEntity.UnitPrice * offerDetail.Allocation.Value;
            offerDetailEntity.Offer.IsSelected = true;
        }

        foreach (var offer in request.Offers)
        {
            offer.TotalPrice = offer.OfferDetails.Sum(od => od.TotalPrice);
            var totalQuantity = offer.OfferDetails.Sum(od => od.Allocation);
            offer.AverageUnitPrice = (decimal)(totalQuantity == 0 ? 0 : offer.TotalPrice / totalQuantity);
        }


        request.State = RequestStateEnum.AllocationCreated;

        _updateRequestRepository.Update(request);

    }

    private bool ValidateAllocations(List<OfferDetailDto> offerDetails, List<Dictionary<string, object>> rows)
    {
        var quantities = rows.Select(r => (r["quantity"])).ToList();

        var columnSums = new decimal[offerDetails[0].Quantity];
        foreach (var offerDetail in offerDetails)
        {
            if (offerDetail.Allocation.HasValue)
            {
                for (int i = 0; i < offerDetail.Quantity; i++)
                {
                    columnSums[i] += offerDetail.Allocation.Value;
                }
            }
        }

        for (int i = 0; i < columnSums.Length; i++)
        {
            if (Math.Abs(columnSums[i] - Convert.ToInt32(quantities[i])) > 0)
            {
                return false;
            }
        }

        return true;
    }

    public async Task RequestRevision(int companyId, int OfferId)
    {
        var offer = await _queryOfferRepository.GetFirstAsync(x => x.Request.CompanyId == companyId && x.Id == OfferId).Include(x => x.Request).FirstAsync()
           ?? throw new InvalidOperationException(_localizationService.GetLocalizedString("NotFoundEntity"));


        if (offer.Request.BidCollectionEndDate < DateTime.UtcNow)
            throw new InvalidOperationException(_localizationService.GetLocalizedString("BidCollectionProcessEnded"));

        int orginalOfferId = offer.OriginalOfferId != null ? offer.OriginalOfferId.Value : offer.Id;

        var pendigRevisedExist = await _queryOfferRepository.IsExisting(x => x.OriginalOfferId == orginalOfferId && x.IsRevised && x.OfferStatus == OfferStatus.PendingOffer);

        if (pendigRevisedExist)
        {
            throw new InvalidOperationException(_localizationService.GetLocalizedString("PendingRevisionRequest"));
        }

        var revisedCount = await _queryOfferRepository.GetList(x => x.OriginalOfferId == orginalOfferId && x.IsRevised).CountAsync() + 1;

        var orginalOffer = await _queryOfferRepository.GetByIdAsync(orginalOfferId);

        var revisedOffer = new Offer()
        {
            RequestId = orginalOffer.RequestId,
            CompanyId = orginalOffer.CompanyId,
            TotalPrice = 0,
            AverageUnitPrice = 0,
            MaturityDays = 0,
            Notes = string.Empty,
            RejectionReason = string.Empty,
            IsRevised = true,
            OfferStatus = OfferStatus.PendingOffer,
            OriginalOfferId = orginalOfferId,
            RevisedOfferId = null,
            AddedToComparisonTable = false,
            AddedToShortList = false,
            AddedToReverseAuction = false,
            ReferenceCode = orginalOffer.ReferenceCode + "-RV" + revisedCount,
        };


        await _addOfferRepository.AddAsync(revisedOffer);
    }

    public async Task UpdateOfferPrices(int companyId, List<UpdateOfferPriceDto> model)
    {
        // Güncellenmesi gereken OfferDetail'ları veritabanından çekiyoruz
        var offerDetailsToUpdate = await _queryOfferRepository.GetList(x => x.CompanyId == companyId &&
            x.OfferDetails.Any(od => model.Select(m => m.OfferDetailId).Contains(od.Id)), true)
            .SelectMany(x => x.OfferDetails)
            .Include(x => x.Offer)
            .Where(od => model.Select(m => m.OfferDetailId).Contains(od.Id))
            .ToListAsync();

        // OfferDetail'ları güncelle
        foreach (var offerDetail in offerDetailsToUpdate)
        {
            var update = model.FirstOrDefault(m => m.OfferDetailId == offerDetail.Id);
            if (update != null)
            {
                // OfferDetail'ın fiyatlarını güncelle
                offerDetail.UnitPrice = update.NewUnitPrice;
                offerDetail.TotalPrice = offerDetail.Allocation == null
                    ? offerDetail.Quantity * update.NewUnitPrice
                    : offerDetail.Allocation.Value * update.NewUnitPrice;

                // Offer'ı güncelle
                var offer = offerDetail.Offer;
                offer.TotalPrice = offer.OfferDetails.Sum(od => od.TotalPrice);
                var totalQuantity = offer.OfferDetails.Sum(od => od.Quantity);
                offer.AverageUnitPrice = totalQuantity == 0 ? 0 : offer.TotalPrice / totalQuantity;
                _updateOfferRepository.Update(offer);
            }
        }

        // Fiyat değişikliklerini bildiren bir servis çağrısı yapıyoruz
        _ = _sendNotificationService.ChangePrice(offerDetailsToUpdate.First().Offer.RequestId.ToString());
    }
}
