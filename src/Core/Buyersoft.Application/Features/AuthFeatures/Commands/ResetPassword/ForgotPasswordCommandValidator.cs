using FluentValidation;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ResetPassword
{
    public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(p => p.token).NotNull().NotEmpty().WithMessage("RequiredField");
        }
    }
}
