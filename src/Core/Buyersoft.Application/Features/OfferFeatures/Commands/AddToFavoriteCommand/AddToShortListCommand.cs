using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.AddToFavorite;

public sealed record AddToFavoriteCommand(int offerId) : ICommand<AddToFavoriteCommandResponse>;