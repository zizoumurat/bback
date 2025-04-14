using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToShortList;

public sealed record RemoveToShortListCommand(int offerId) : ICommand<RemoveToShortListCommandResponse>;