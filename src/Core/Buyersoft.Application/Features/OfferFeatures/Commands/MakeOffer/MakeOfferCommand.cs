using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.MakeOffer;

public sealed record MakeOfferCommand(MakeOfferDto MakeOffer) : ICommand<MakeOfferCommandResponse>;