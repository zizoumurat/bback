using FluentValidation;

namespace Buyersoft.Application.Features.RequestGroupFeatures.Commands.CreateRequestGroup;
public class CreateRequestGroupCommandValidator : AbstractValidator<CreateRequestGroupCommand>
{
    public CreateRequestGroupCommandValidator()
    {
        RuleFor(p => p.RequestGroup.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
