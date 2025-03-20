using FluentValidation;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.CreateMainCategory;
public class CreateMainCategoryCommandValidator : AbstractValidator<CreateMainCategoryCommand>
{
    public CreateMainCategoryCommandValidator()
    {
        RuleFor(p => p.MainCategory.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
