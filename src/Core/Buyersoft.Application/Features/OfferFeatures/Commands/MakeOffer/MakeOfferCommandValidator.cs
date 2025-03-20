using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.MakeOffer;
public class MakeOfferCommandValidator : AbstractValidator<MakeOfferCommand>
{
    public MakeOfferCommandValidator()
    {
        RuleFor(p => p.MakeOffer.PriceList).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.MakeOffer.RequestId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.MakeOffer.MaturityDays).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
