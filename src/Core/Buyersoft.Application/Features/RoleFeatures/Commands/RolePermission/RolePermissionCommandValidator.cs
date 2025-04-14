using FluentValidation;

namespace Buyersoft.Application.Features.RoleFeatures.Commands.RolePermission;
public class RolePermissionCommandValidator : AbstractValidator<RolePermissionCommand>
{
    public RolePermissionCommandValidator()
    {
        RuleFor(p => p.RolePermission.RoleId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.RolePermission.PermissionIdList).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
