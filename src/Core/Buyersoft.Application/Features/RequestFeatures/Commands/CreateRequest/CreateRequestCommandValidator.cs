using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.CreateRequest;
public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
    public CreateRequestCommandValidator()
    {
        RuleFor(p => p.Request.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
