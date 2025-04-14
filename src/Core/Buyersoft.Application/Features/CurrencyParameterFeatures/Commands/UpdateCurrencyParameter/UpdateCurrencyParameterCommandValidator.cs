using FluentValidation;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.UpdateCurrencyParameter;
public class UpdateCurrencyParameterCommandValidator : AbstractValidator<UpdateCurrencyParameterCommand>
{
    public UpdateCurrencyParameterCommandValidator()
    {
        RuleFor(p => p.CurrencyParameter.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.Currency1Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.Currency2Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.ExchangeRate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.StartDate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CurrencyParameter.ExpiredDate).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
