using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferFeatures.Commands.RejectOffer;

public sealed record RejectOfferCommand(RejectOfferDto Model) : ICommand<RejectOfferCommandResponse>;