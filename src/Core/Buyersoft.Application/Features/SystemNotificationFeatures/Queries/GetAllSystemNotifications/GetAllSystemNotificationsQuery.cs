using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Queries.GetAllSystemNotifications;
public sealed record GetAllSystemNotificationsQuery(SystemNotificationFilterDto filter, PageRequest pagination) : IQuery<GetAllSystemNotificationsQueryResponse>;
