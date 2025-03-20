using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.CreateApprovalChain;

public sealed record CreateApprovalChainCommand(ApprovalChainDto ApprovalChain) : ICommand<CreateApprovalChainCommandResponse>;
 