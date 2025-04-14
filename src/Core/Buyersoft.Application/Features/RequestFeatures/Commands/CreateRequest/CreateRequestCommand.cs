using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CreateRequest;

public sealed record CreateRequestCommand(CreateRequestDto Request) : ICommand<CreateRequestCommandResponse>;