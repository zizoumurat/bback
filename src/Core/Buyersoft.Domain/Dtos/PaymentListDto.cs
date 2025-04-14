using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public class PaymentListDto()
{
    public int Id { get; set; }
    public string PaymentListCode { get; set; }
    public string Subject { get; set; }
    public decimal TotalPrice { get; set; }
    public ApprovalStatus Status { get; set; }
    public List<ApprovalUser> ApprovalUsers { get; set; }
    public List<OrderListDto> Orders { get; set; }
}

public sealed record ApprovalUser(int Id, string UserName, ApprovalStatus Status);

public sealed record PaymentListCreateDto(int Id, string Subject, List<int> OrderIdList);

public sealed record ApproveRejectPaymentListDto(int Id, string Comment, ApprovalStatus Status);

public sealed record PaymentListFilterDto(string Name, ApprovalStatus? Status);