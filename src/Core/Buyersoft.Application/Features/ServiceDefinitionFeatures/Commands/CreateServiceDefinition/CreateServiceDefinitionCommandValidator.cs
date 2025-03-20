using FluentValidation;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.CreateServiceDefinition;
public class CreateServiceDefinitionCommandValidator : AbstractValidator<CreateServiceDefinitionCommand>
{
    public CreateServiceDefinitionCommandValidator()
    {
        RuleFor(p => p.ServiceDefinition.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ServiceDefinition.CategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
