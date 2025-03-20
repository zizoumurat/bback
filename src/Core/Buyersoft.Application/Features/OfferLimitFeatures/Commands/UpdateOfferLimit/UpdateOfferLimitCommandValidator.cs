using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.UpdateOfferLimit;
public class UpdateOfferLimitCommandValidator : AbstractValidator<UpdateOfferLimitCommand>
{
    public UpdateOfferLimitCommandValidator()
    {
        RuleFor(p => p.OfferLimit.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.OfferLimit.SpendLimit).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.OfferLimit.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.OfferLimit.MinimumOfferCount).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
