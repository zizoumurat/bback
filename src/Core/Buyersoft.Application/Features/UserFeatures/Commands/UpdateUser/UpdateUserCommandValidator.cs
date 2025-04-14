using FluentValidation;

namespace Buyersoft.Application.Features.UserFeatures.Commands.UpdateUser;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(p => p.User.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Name).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Surname).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Email).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.RoleId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
