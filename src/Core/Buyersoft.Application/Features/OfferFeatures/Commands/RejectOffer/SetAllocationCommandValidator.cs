using FluentValidation;

namespace Buyersoft.Application.Features.OfferFeatures.Commands.RejectOffer;
public class RejectOfferCommandValidator : AbstractValidator<RejectOfferCommand>
{
    public RejectOfferCommandValidator()
    {
        RuleFor(p => p.Model.RequestId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Model.RejectionReason).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
