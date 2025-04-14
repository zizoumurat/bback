using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ContractFeatures.Commands.ApproveReject;

public sealed record ApproveRejectContractCommand(ApproveRejectContractDto Request) : ICommand<ApproveRejectContractCommandResponse>;