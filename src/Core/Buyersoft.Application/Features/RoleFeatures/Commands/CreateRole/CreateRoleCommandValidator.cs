using FluentValidation;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.CreateRole;
public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(p => p.Role.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
