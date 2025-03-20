using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.AddToShortList;
public class AddToShortListCommandValidator : AbstractValidator<AddToShortListCommand>
{
    public AddToShortListCommandValidator()
    {
        RuleFor(p => p.offerId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
