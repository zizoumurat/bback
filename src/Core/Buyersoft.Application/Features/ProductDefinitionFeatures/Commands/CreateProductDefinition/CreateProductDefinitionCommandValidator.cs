using FluentValidation;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.CreateProductDefinition;
public class CreateProductDefinitionCommandValidator : AbstractValidator<CreateProductDefinitionCommand>
{
    public CreateProductDefinitionCommandValidator()
    {
        RuleFor(p => p.ProductDefinition.Code).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ProductDefinition.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ProductDefinition.CategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
