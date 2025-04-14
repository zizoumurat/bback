using Buyersoft.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Buyersoft.Domain.Dtos;
public record BudgetDto(
    int Id,
    int CompanyId,
    int CurrencyId,
    DateOnly StartDate,
    DateOnly EndDate,
    int DepartmentId,
    decimal BudgetLimit,
    decimal AvailableLimit,
    string BudgetTitle
 );

public sealed record BudgetFilterDto(
     decimal? BudgetLimitMax,
     decimal? BudgetLimitMin,
     string BudgetTitle,
     int? DepartmentId,
     int? CompanyId,
     int? CurrencyId,
     decimal?  AvailableLimit,
     DateOnly? StartDate,
     DateOnly? EndDate
);

public class BudgetListDto()
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int CurrencyId { get; set; }
    public decimal BudgetLimit { get; set; }
    public decimal AvailableLimit { get; set; }
    public string BudgetTitle { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string DepartmentName { get; set; }
    public string CurrencyName { get; set; }
    public string CurrencyCode { get; set; }
}



public class ContractListDto()
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string ReferenceCode { get; set; }
    public decimal TotalPrice { get; set; }
    public string Supplier { get; set; }
    public string Requester { get; set; }
    public string Owner { get; set; }
    public string DocumentUrl { get; set; }
    public string DocumentName { get; set; }
    public string MimeType { get; set; }
    public string Currency { get; set; }
    public ContractStatus ContractStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}

public sealed record ContractFilterDto(
     string RequestTitle,
     int? CurrencyId,
     decimal? TotalPrice,
     DateOnly? StartDate,
     DateOnly? ExpirationDate
);

public sealed record UploadContractFileDto(int Id, DateTime? StartDate, DateTime? ExpirationDate, IFormFile File);

public sealed record ApproveRejectContractDto(int Id, string Comment, ApprovalStatus Status);

public sealed record CommentListDto(int UserId,string User,string Comment);
public sealed record AddCommentDto(string Comment, int ContractId);