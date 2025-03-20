using FluentValidation;

namespace Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompanyLogo;
public class UpdateCompanyLogoCommandValidator : AbstractValidator<UpdateCompanyLogoCommand>
{
    public UpdateCompanyLogoCommandValidator()
    {
        RuleFor(p => p.File).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
