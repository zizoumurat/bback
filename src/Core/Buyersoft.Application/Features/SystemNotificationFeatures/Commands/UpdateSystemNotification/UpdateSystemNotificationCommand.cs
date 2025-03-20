using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.UpdateSystemNotification;

public sealed record UpdateSystemNotificationCommand(SystemNotificationDto SystemNotification) : ICommand<UpdateSystemNotificationCommandResponse>;