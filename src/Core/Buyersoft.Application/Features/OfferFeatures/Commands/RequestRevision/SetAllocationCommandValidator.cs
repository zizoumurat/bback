using FluentValidation;

namespace Buyersoft.Application.Features.OfferFeatures.Commands.RequestRevision;
public class RequestRevisionCommandValidator : AbstractValidator<RequestRevisionCommand>
{
    public RequestRevisionCommandValidator()
    {
        RuleFor(p => p.OfferId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
