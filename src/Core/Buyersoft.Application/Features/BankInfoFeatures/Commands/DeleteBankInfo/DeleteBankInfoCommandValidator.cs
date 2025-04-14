using FluentValidation;

namespace Buyersoft.Application.Features.BankInfoFeatures.Commands.DeleteBankInfo;

public class DeleteBankInfoCommandValidator : AbstractValidator<DeleteBankInfoCommand>
{
    public DeleteBankInfoCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
