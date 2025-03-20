using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.CancelOrder;

public sealed record CancelOrderCommand(CancelOrderDto Model) : ICommand<CancelOrderCommandResponse>;