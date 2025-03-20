using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.ChangePrices;

public sealed record ChangePricesCommand(List<UpdateOfferPriceDto> Model) : ICommand<ChangePricesCommandResponse>;