using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToShortList;
public class RemoveToShortListCommandValidator : AbstractValidator<RemoveToShortListCommand>
{
    public RemoveToShortListCommandValidator()
    {
        RuleFor(p => p.offerId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
