using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record CreateRequestDto(
    int Id,
    string Title,
    string Reason,
    decimal Amount,
    bool BudgetInclusionStatus,
    DateTime? EstimatedSupplyDate,
    DateTime RequestedSupplyDate,
    int CurrencyId,
    int CategoryId,
    CollectionChannel? CollectionChannel,
    int? BudgetId,
    int TemplateId);

public sealed record RequestDto(
    int Id,
    string Title,
    int LocationId,
    decimal Amount,
    DateTime RequestedSupplyDate,
    DateTime? EstimatedSupplyDate,
    CollectionChannel? CollectionChannel,
    string Reason,
    int CurrencyId,
    RequestType RequestType,
    int TemplateId,
    int CreatedById,
    int? ManagerId,
    ApprovalStatus Status,
    DateTime? ApprovedDate,
    int? BudgetId
);

public sealed record RequestFilterDto(
    int? CurrencyId,
    int? CityId,
    int? UserId,
    int? Total,
    int? CompanyId,
    int? CategoryId,
    int? TemplateId,
    int? CreatedById,
    int? ManagerId,
    int? BudgetId,
    int? LocationId,
    bool? IsApproved,
    bool? IsRevised,
    bool? IsReverseAuction,
    bool? IsForApproval,
    bool? IsForApprovalArchive,
    string RequestCode,
    string Title,
    string Reason,
    CollectionChannel? CollectionChannel,
    RequestStateEnum? State,
    bool? BudgetInclusionStatus,
    DateTime? RequestedSupplyDate,
    DateTime? EstimatedSupplyDate
);

public class RequestListDto()
{
    public int Id { get; set; }
    public int OfferId { get; set; }
    public string CompanyName { get; set; }
    public string Title { get; set; }
    public string RequestCode { get; set; }
    public string OfferCode { get; set; }
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public decimal Amount { get; set; }
    public DateTime? RequestedSupplyDate { get; set; }
    public DateTime? EstimatedSupplyDate { get; set; }
    public DateTime? BidCollectionEndDate { get; set; }

    public CollectionChannel? CollectionChannel { get; set; }
    public string Reason { get; set; }
    public int CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public string Code { get; set; }
    public RequestType? RequestType { get; set; }
    public int TemplateId { get; set; }
    public int CreatedById { get; set; }
    public string CreatedBy { get; set; }
    public int? ManagerId { get; set; }
    public string Manager { get; set; }
    public List<string> OwnerUserList { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public int? BudgetId { get; set; }
    public string BudgetName { get; set; }
    public bool BudgetInclusionStatus { get; set; }
    public int[] CategoryUsers { get; set; }
    public int CategoryId { get; set; }
    public int MainCategoryId { get; set; }
    public int SubCategoryId { get; set; }
    public int RequestGroupId { get; set; }
    public int? ReverseAuctionId { get; set; }
    public RequestStateEnum State { get; set; }
    public string CommercialEvaluation { get; set; }
    public string TechnicalEvaluation { get; set; }
    public TemplateDto Template { get; set; }
}

public class StartBidCollectionDto
{
    public CreateRequestDto Request { get; set; }
    public List<int> ProviderIdList { get; set; }
}

public class CancelBidCollectionDto
{
    public int Id { get; set; }
    public string CancellationReasion { get; set; }
}

public sealed record AssignManagerDto(int Id);
public sealed record StartApprovalProcessDto(int Id, string TechnicalEvaluation, string CommercialEvaluation);
public sealed record ApproveRejectRequestDto(int Id, string Comment, ApprovalStatus Status);