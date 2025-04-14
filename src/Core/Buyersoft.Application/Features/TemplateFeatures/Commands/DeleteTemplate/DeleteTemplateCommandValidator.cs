using FluentValidation;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.DeleteTemplate;

public class DeleteTemplateCommandValidator : AbstractValidator<DeleteTemplateCommand>
{
    public DeleteTemplateCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
