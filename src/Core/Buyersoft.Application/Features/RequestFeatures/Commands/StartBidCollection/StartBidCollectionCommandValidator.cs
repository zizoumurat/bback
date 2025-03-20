using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.StartBidCollection;
public class StartBidCollectionCommandValidator : AbstractValidator<StartBidCollectionCommand>
{
    public StartBidCollectionCommandValidator()
    {
        RuleFor(p => p.Request.Request).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Request.ProviderIdList).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
