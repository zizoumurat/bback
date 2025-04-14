using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CreateComprasionTable;

public sealed record CreateComprasionTableCommand(int requestId, int offerType) : ICommand<CreateComprasionTableCommandResponse>;