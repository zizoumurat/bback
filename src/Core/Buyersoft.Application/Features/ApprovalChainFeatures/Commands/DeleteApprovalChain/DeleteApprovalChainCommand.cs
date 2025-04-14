using Buyersoft.Application.Features.ApprovalChainFeatures.Commands.DeleteApprovalChain;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.DeleteApprovalChain;
public sealed record DeleteApprovalChainCommand(int Id) : ICommand<DeleteApprovalChainCommandResponse>;
