namespace Buyersoft.Domain.Dtos;

public sealed record OrderCreateDto(int OrderPreparationId, List<OrderItemCreateDto> OrderItems);


