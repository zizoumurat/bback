using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record OrderListDto(int Id,  string OrderCode, decimal TotalPrice, OrderStatusEnum Status, DateTime OrderDate, List<OrderItemListDto> OrderItems);

public class OrderPaginationDto
{
    public int Id { get; init; }
    public string OrderCode { get; init; }
    public decimal TotalPrice { get; init; }
    public OrderStatusEnum Status { get; init; }
    public DateTime OrderDate { get; init; }
    public List<OrderItemListDto> OrderItems { get; init; } = new();
    public OrderPreparationListDto OrderPreparation { get; init; }
}


public sealed record SetNonconformityDto(int Id, string Detail, NonconformityReasonEnum Status);
public sealed record ChangeOrderStatusDto(int Id, string InvoiceNumber, string WaybillNumber, NonconformityReasonEnum Status);
public sealed record CancelOrderDto(int Id);
public sealed record DeliveredOrderDto(int Id);

