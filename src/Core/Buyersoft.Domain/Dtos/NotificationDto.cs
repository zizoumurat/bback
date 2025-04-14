namespace Buyersoft.Domain.Dtos;

public sealed record NotificationDto(int Id, int UserId, string Message, bool Read);
