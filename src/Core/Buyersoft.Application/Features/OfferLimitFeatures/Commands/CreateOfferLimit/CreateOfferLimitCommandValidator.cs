using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.CreateOfferLimit;
public class CreateOfferLimitCommandValidator : AbstractValidator<CreateOfferLimitCommand>
{
    public CreateOfferLimitCommandValidator()
    {
        RuleFor(p => p.OfferLimit.SpendLimit).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.OfferLimit.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.OfferLimit.MinimumOfferCount).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
