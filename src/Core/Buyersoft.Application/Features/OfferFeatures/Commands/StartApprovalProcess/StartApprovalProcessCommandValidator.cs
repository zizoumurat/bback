using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.StartApprovalProcess;
public class StartApprovalProcessCommandValidator : AbstractValidator<StartApprovalProcessCommand>
{
    public StartApprovalProcessCommandValidator()
    {
        RuleFor(p => p.Model.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Model.CommercialEvaluation).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Model.TechnicalEvaluation).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
