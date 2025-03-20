using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.StartApprovalProcess;

public sealed record StartApprovalProcessCommand(StartApprovalProcessDto Model) : ICommand<StartApprovalProcessCommandResponse>;