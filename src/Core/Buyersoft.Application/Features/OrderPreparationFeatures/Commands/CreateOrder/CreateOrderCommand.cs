using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OrderPreparationFeatures.Commands.CreateOrder;

public sealed record CreateOrderCommand(OrderCreateDto Order) : ICommand<CreateOrderCommandResponse>;