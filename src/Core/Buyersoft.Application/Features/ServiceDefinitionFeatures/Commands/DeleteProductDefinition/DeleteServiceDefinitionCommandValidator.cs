using FluentValidation;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.DeleteServiceDefinition;

public class DeleteServiceDefinitionCommandValidator : AbstractValidator<DeleteServiceDefinitionCommand>
{
    public DeleteServiceDefinitionCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
