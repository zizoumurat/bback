using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferFeatures.Commands.RequestRevision;

public sealed record RequestRevisionCommand(int CompanyId, int OfferId) : ICommand<RequestRevisionCommandResponse>;