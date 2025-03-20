using FluentValidation;

namespace Buyersoft.Application.Features.CompanySubCategoryFeatures.Commands.CreateCompanySubCategory;
public class CreateCompanySubCategoryCommandValidator : AbstractValidator<CreateCompanySubCategoryCommand>
{
    public CreateCompanySubCategoryCommandValidator()
    {
        RuleFor(p => p.CompanySubCategory.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
