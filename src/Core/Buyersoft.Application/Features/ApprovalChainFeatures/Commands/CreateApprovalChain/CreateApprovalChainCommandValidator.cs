using FluentValidation;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.CreateApprovalChain;
public class CreateApprovalChainCommandValidator : AbstractValidator<CreateApprovalChainCommand>
{
    public CreateApprovalChainCommandValidator()
    {
        RuleFor(p => p.ApprovalChain.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ApprovalChain.SpendLimit).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
