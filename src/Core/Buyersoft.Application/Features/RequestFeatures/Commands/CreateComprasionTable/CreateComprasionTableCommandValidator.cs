using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CreateComprasionTable;
public class CreateComprasionTableCommandValidator : AbstractValidator<CreateComprasionTableCommand>
{
    public CreateComprasionTableCommandValidator()
    {
        RuleFor(p => p.offerType).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.requestId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
