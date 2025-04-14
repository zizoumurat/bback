using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.CreateSystemNotification;

public sealed record CreateSystemNotificationCommand(SystemNotificationDto SystemNotification) : ICommand<CreateSystemNotificationCommandResponse>;