using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.UpdateRequest;
public class UpdateRequestCommandValidator : AbstractValidator<UpdateRequestCommand>
{
    public UpdateRequestCommandValidator()
    {
        RuleFor(p => p.Request.CurrencyId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
