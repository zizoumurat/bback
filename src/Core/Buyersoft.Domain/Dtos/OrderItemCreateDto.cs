namespace Buyersoft.Domain.Dtos;

public sealed record OrderItemCreateDto(int OfferDetailId, int Quantity, int UnitPrice, string ProductDefinition);


