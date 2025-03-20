using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.AssignManager;
public class AssignManagerCommandValidator : AbstractValidator<AssignManagerCommand>
{
    public AssignManagerCommandValidator()
    {
        RuleFor(p => p.Request.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
