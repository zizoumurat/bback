using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.DeliveredOrder;

public sealed record DeliveredOrderCommand(DeliveredOrderDto Model) : ICommand<DeliveredOrderCommandResponse>;