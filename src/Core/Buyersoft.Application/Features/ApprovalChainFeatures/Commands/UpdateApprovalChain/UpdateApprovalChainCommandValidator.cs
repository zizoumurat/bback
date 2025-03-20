using FluentValidation;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.UpdateApprovalChain;
public class UpdateApprovalChainCommandValidator : AbstractValidator<UpdateApprovalChainCommand>
{
    public UpdateApprovalChainCommandValidator()
    {
        RuleFor(p => p.ApprovalChain.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ApprovalChain.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ApprovalChain.SpendLimit).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
