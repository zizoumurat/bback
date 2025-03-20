using FluentValidation;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.UpdateBudget;
public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
{
    public UpdateBudgetCommandValidator()
    {
        RuleFor(p => p.Budget.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.BudgetTitle).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.BudgetLimit).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.DepartmentId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.StartDate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.EndDate).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
