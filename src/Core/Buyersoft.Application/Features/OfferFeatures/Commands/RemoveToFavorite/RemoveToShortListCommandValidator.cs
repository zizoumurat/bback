using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.RemoveToFavorite;
public class RemoveToFavoriteCommandValidator : AbstractValidator<RemoveToFavoriteCommand>
{
    public RemoveToFavoriteCommandValidator()
    {
        RuleFor(p => p.offerId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
