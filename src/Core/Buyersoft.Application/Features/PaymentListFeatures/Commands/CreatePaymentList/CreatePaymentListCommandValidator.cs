using FluentValidation;

namespace Buyersoft.Application.Features.PaymentListFeatures.Commands.CreatePaymentList;
public class CreatePaymentListCommandValidator : AbstractValidator<CreatePaymentListCommand>
{
    public CreatePaymentListCommandValidator()
    {
        RuleFor(p => p.PaymentList.OrderIdList).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.PaymentList.Subject).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
