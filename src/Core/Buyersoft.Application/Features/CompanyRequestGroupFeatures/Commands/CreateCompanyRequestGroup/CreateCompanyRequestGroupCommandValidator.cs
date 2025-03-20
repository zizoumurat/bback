using FluentValidation;

namespace Buyersoft.Application.Features.CompanyRequestGroupFeatures.Commands.CreateCompanyRequestGroup;
public class CreateCompanyRequestGroupCommandValidator : AbstractValidator<CreateCompanyRequestGroupCommand>
{
    public CreateCompanyRequestGroupCommandValidator()
    {
        RuleFor(p => p.CompanyRequestGroup.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
