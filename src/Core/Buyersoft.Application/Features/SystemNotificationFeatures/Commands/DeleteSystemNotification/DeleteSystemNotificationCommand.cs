﻿using Buyersoft.Application.Features.SystemNotificationFeatures.Commands.DeleteSystemNotification;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.DeleteSystemNotification;
public sealed record DeleteSystemNotificationCommand(int Id) : ICommand<DeleteSystemNotificationCommandResponse>;
