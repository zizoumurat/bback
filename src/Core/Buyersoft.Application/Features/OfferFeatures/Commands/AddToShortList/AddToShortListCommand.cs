using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.AddToShortList;

public sealed record AddToShortListCommand(int offerId) : ICommand<AddToShortListCommandResponse>;