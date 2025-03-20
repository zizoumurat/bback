using FluentValidation;

namespace Buyersoft.Application.Features.BudgetFeatures.Commands.CreateBudget;
public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        RuleFor(p => p.Budget.BudgetTitle).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.BudgetLimit).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.DepartmentId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.StartDate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Budget.EndDate).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
