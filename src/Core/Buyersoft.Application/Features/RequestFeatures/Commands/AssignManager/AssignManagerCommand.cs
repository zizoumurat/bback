using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.AssignManager;

public sealed record AssignManagerCommand(AssignManagerDto Request) : ICommand<AssignManagerCommandResponse>;