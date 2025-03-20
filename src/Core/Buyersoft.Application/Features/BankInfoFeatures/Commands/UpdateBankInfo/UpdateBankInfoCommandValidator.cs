using FluentValidation;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.UpdateBankInfo;
public class UpdateBankInfoCommandValidator : AbstractValidator<UpdateBankInfoCommand>
{
    public UpdateBankInfoCommandValidator()
    {
        RuleFor(p => p.BankInfo.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.BankInfo.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.BankInfo.IBAN).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
