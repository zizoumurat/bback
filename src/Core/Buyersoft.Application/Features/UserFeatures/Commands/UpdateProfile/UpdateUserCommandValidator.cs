using FluentValidation;

namespace Buyersoft.Application.Features.UserFeatures.Commands.UpdateProfile;
public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(p => p.User.Name).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Surname).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.Email).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.PhoneNumber).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.User.ChoosenLanguage).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
