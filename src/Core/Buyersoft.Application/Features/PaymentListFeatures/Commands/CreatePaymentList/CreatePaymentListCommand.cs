using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.PaymentListFeatures.Commands.CreatePaymentList;

public sealed record CreatePaymentListCommand(PaymentListCreateDto PaymentList) : ICommand<CreatePaymentListCommandResponse>;