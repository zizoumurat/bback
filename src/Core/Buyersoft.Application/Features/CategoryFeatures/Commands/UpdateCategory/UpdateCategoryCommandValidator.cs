using FluentValidation;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.UpdateCategory;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(p => p.Category.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Category.MainCategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Category.SubCategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Category.RequestGroupId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Category.LocationId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
