using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.UpdateApprovalChain;

public sealed record UpdateApprovalChainCommand(ApprovalChainDto ApprovalChain) : ICommand<UpdateApprovalChainCommandResponse>;