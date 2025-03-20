using FluentValidation;

namespace Buyersoft.Application.Features.UserFeatures.Commands.CreateUser;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.User.Name).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Surname).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Email).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.RoleId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Password).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
