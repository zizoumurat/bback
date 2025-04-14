using FluentValidation;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.DeleteProductDefinition;

public class DeleteProductDefinitionCommandValidator : AbstractValidator<DeleteProductDefinitionCommand>
{
    public DeleteProductDefinitionCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
