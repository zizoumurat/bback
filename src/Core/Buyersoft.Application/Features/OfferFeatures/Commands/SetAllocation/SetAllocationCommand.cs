using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.SetAllocation;

public sealed record SetAllocationCommand(int RequestId, List<OfferDetailDto> OfferDetailList) : ICommand<SetAllocationCommandResponse>;