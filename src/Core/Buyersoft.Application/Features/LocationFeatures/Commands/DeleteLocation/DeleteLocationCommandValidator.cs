using FluentValidation;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.DeleteLocation;

public class DeleteLocationCommandValidator : AbstractValidator<DeleteLocationCommand>
{
    public DeleteLocationCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
