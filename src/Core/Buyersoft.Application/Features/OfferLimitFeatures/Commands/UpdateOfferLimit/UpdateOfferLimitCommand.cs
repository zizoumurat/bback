using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.UpdateOfferLimit;

public sealed record UpdateOfferLimitCommand(OfferLimitDto OfferLimit) : ICommand<UpdateOfferLimitCommandResponse>;