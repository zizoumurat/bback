using FluentValidation;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.CreateTemplate;
public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
{
    public CreateTemplateCommandValidator()
    {
        RuleFor(p => p.Template.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
