using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.ChangeOrderStatus;

public sealed record ChangeOrderStatusCommand(ChangeOrderStatusDto Model) : ICommand<ChangeOrderStatusCommandResponse>;