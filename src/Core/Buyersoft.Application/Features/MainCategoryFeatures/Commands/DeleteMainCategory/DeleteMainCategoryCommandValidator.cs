using FluentValidation;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.DeleteMainCategory;

public class DeleteMainCategoryCommandValidator : AbstractValidator<DeleteMainCategoryCommand>
{
    public DeleteMainCategoryCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
