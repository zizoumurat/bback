using FluentValidation;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.CreateLocation;
public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(p => p.Location.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
