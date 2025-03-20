using FluentValidation;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.DeleteSystemNotification;

public class DeleteSystemNotificationCommandValidator : AbstractValidator<DeleteSystemNotificationCommand>
{
    public DeleteSystemNotificationCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
