using FluentValidation;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.CreateBankInfo;
public class CreateBankInfoCommandValidator : AbstractValidator<CreateBankInfoCommand>
{
    public CreateBankInfoCommandValidator()
    {
        RuleFor(p => p.BankInfo.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
