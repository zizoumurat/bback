using FluentValidation;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.UpdateProductDefinition;
public class UpdateProductDefinitionCommandValidator : AbstractValidator<UpdateProductDefinitionCommand>
{
    public UpdateProductDefinitionCommandValidator()
    {
        RuleFor(p => p.ProductDefinition.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ProductDefinition.Code).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ProductDefinition.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ProductDefinition.CategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
