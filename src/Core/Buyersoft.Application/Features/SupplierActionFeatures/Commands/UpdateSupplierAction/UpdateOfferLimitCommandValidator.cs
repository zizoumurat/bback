using FluentValidation;

namespace Buyersoft.Application.Features.SupplierActionFeatures.Commands.UpdateSupplierAction;
public class UpdateSupplierActionCommandValidator : AbstractValidator<UpdateSupplierActionCommand>
{
    public UpdateSupplierActionCommandValidator()
    {
        RuleFor(p => p.SupplierAction.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.SupplierAction.SupplierNotes).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.SupplierAction.SupplierActionStatus).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
