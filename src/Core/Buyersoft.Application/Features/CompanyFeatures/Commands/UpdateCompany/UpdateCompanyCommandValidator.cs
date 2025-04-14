using FluentValidation;

namespace Buyersoft.Application.Features.CompanyFeatures.Commands.UpdateCompany;
public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(p => p.Company.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Company.Name).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Company.Address).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Company.Phone).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Company.Email).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Company.TaxAdministration).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Company.CityId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
