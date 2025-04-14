using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.ApproveReject;

public sealed record ApproveRejectCommand(ApproveRejectRequestDto Request) : ICommand<ApproveRejectCommandResponse>;