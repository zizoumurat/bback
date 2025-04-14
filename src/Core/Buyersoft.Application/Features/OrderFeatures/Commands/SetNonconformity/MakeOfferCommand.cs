using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.SetNonconformity;

public sealed record SetNonconformityCommand(SetNonconformityDto Model) : ICommand<SetNonconformityCommandResponse>;