using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Queries.GetAllSystemNotifications;

public sealed record GetAllSystemNotificationsQueryResponse(PaginatedList<SystemNotificationListDto> result);
