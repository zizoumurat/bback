using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.ChangePrices;
public class ChangePricesCommandValidator : AbstractValidator<ChangePricesCommand>
{
    public ChangePricesCommandValidator()
    {
        RuleFor(p => p.Model).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
