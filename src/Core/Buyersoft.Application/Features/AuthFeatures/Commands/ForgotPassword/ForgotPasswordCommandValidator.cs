using FluentValidation;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.ForgotPassword
{
    public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(p => p.email).NotNull().NotEmpty().WithMessage("RequiredField");
        }
    }
}
