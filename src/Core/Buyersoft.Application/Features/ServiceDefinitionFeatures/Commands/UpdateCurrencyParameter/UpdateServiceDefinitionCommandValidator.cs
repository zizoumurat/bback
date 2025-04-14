using FluentValidation;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.UpdateServiceDefinition;
public class UpdateServiceDefinitionCommandValidator : AbstractValidator<UpdateServiceDefinitionCommand>
{
    public UpdateServiceDefinitionCommandValidator()
    {
        RuleFor(p => p.ServiceDefinition.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ServiceDefinition.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ServiceDefinition.CategoryId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
