using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CancelBidCollection;
public class CancelBidCollectionCommandValidator : AbstractValidator<CancelBidCollectionCommand>
{
    public CancelBidCollectionCommandValidator()
    {
        RuleFor(p => p.Model.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Model.CancellationReasion).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
