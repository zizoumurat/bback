using FluentValidation;

namespace Buyersoft.Application.Features.AppFeatures.AuthFeatures.Commands.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(p => p.email).NotNull().NotEmpty().WithMessage("E-posta boş bırakılamaz!");
            RuleFor(p => p.password).NotNull().NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }
}
