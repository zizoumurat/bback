using FluentValidation;

namespace Buyersoft.Application.Features.OfferLimitFeatures.Commands.DeleteOfferLimit;

public class DeleteOfferLimitCommandValidator : AbstractValidator<DeleteOfferLimitCommand>
{
    public DeleteOfferLimitCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
