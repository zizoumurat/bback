using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.StartBidCollection;

public sealed record StartBidCollectionCommand(StartBidCollectionDto Request) : ICommand<StartBidCollectionCommandResponse>;