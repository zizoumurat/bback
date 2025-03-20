using FluentValidation;

namespace Buyersoft.Application.Features.SubCategoryFeatures.Commands.CreateSubCategory;
public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
{
    public CreateSubCategoryCommandValidator()
    {
        RuleFor(p => p.SubCategory.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
