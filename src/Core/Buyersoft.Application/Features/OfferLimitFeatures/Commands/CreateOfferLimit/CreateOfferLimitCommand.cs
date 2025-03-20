using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.CreateOfferLimit;

public sealed record CreateOfferLimitCommand(OfferLimitDto OfferLimit) : ICommand<CreateOfferLimitCommandResponse>;