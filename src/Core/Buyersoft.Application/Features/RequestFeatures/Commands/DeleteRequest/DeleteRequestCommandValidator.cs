using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.DeleteRequest;

public class DeleteRequestCommandValidator : AbstractValidator<DeleteRequestCommand>
{
    public DeleteRequestCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
