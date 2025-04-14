using FluentValidation;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.UpdateMainCategory;
public class UpdateMainCategoryCommandValidator : AbstractValidator<UpdateMainCategoryCommand>
{
    public UpdateMainCategoryCommandValidator()
    {
        RuleFor(p => p.MainCategory.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.MainCategory.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
