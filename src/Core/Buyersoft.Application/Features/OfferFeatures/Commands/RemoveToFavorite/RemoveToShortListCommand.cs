using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToFavorite;

public sealed record RemoveToFavoriteCommand(int offerId) : ICommand<RemoveToFavoriteCommandResponse>;