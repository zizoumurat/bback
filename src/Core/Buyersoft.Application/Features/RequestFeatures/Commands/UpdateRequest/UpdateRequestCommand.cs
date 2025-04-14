using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Enums;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.UpdateRequest;

public sealed record UpdateRequestCommand(CreateRequestDto Request) : ICommand<UpdateRequestCommandResponse>;