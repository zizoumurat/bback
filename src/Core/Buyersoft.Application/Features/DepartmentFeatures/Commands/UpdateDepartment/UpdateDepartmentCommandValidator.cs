using FluentValidation;

namespace Buyersoft.Application.Features.DepartmentFeatures.Commands.UpdateDepartment;
public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(p => p.Department.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Department.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
