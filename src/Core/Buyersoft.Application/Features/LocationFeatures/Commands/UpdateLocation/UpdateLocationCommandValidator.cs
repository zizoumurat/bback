using FluentValidation;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.UpdateLocation;
public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(p => p.Location.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Location.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
