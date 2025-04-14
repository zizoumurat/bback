namespace Buyersoft.Domain.Dtos;

public sealed record OrderItemListDto(int Id, int OfferDetailId, string ProductDefinition, decimal UnitPrice, decimal TotalPrice, int Quantity);


