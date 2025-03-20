using FluentValidation;

namespace Buyersoft.Application.Features.UserFeatures.Commands.ChangePassword;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(p => p.User.Password).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.NewPassword).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
