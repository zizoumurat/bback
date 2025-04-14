using FluentValidation;

namespace Buyersoft.Application.Features.DepartmentFeatures.Commands.CreateDepartment;
public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(p => p.Department.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
