using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.SetAllocation;
public class SetAllocationCommandValidator : AbstractValidator<SetAllocationCommand>
{
    public SetAllocationCommandValidator()
    {
        RuleFor(p => p.OfferDetailList).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.RequestId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
