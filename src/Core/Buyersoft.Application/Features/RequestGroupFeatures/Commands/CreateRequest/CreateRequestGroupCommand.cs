using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestGroupFeatures.Commands.CreateRequestGroup;

public sealed record CreateRequestGroupCommand(RequestGroupDto RequestGroup) : ICommand<CreateRequestGroupCommandResponse>;