using FluentValidation;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.CreateCategory;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(p => p.Category.MainCategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Category.SubCategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Category.RequestGroupId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Category.LocationId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
