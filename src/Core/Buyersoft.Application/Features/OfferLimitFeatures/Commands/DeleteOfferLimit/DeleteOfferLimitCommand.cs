using Buyersoft.Application.Features.OfferLimitFeatures.Commands.DeleteOfferLimit;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.DeleteOfferLimit;
public sealed record DeleteOfferLimitCommand(int Id) : ICommand<DeleteOfferLimitCommandResponse>;
