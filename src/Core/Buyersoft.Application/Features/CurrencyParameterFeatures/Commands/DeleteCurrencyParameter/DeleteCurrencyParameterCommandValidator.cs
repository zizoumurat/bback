using FluentValidation;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.DeleteCurrencyParameter;

public class DeleteCurrencyParameterCommandValidator : AbstractValidator<DeleteCurrencyParameterCommand>
{
    public DeleteCurrencyParameterCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
