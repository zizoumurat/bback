using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.AddToFavorite;
public class AddToFavoriteCommandValidator : AbstractValidator<AddToFavoriteCommand>
{
    public AddToFavoriteCommandValidator()
    {
        RuleFor(p => p.offerId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
