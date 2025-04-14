namespace Buyersoft.Domain.Dtos;

public sealed record OrderCreateDto(int OrderPreparationId, string ShippingAddress, string Incoterms, DateTime DesiredShippingDate,  List<OrderItemCreateDto> OrderItems);