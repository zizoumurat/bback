using FluentValidation;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
