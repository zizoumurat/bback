using FluentValidation;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Commands.CreateSupplierAction;
public class CreateSupplierActionCommandValidator : AbstractValidator<CreateSupplierActionCommand>
{
    public CreateSupplierActionCommandValidator()
    {
        RuleFor(p => p.SupplierAction.Subject).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.SupplierAction.SupplierId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.SupplierAction.Type).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
