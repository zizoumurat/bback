using FluentValidation;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.DeleteBudget;

public class DeleteBudgetCommandValidator : AbstractValidator<DeleteBudgetCommand>
{
    public DeleteBudgetCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
