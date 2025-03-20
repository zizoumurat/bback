using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CancelBidCollection;

public sealed record CancelBidCollectionCommand(CancelBidCollectionDto Model) : ICommand<CancelBidCollectionCommandResponse>;