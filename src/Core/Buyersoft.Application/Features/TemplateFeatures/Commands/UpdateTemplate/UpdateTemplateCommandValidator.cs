using FluentValidation;

namespace Buyersoft.Application.Features.TemplateFeatures.Commands.UpdateTemplate;
public class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
{
    public UpdateTemplateCommandValidator()
    {
        RuleFor(p => p.Template.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Template.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
