using FluentValidation;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.CreateCurrencyParameter;
public class CreateCurrencyParameterCommandValidator : AbstractValidator<CreateCurrencyParameterCommand>
{
    public CreateCurrencyParameterCommandValidator()
    {
        RuleFor(p => p.CurrencyParameter.Currency1Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.Currency2Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.ExchangeRate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.StartDate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.ExpiredDate).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
