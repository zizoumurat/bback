using FluentValidation;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.UpdateRole;
public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(p => p.Role.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Role.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
