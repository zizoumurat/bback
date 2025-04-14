using FluentValidation;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.CreateSystemNotification;
public class CreateSystemNotificationCommandValidator : AbstractValidator<CreateSystemNotificationCommand>
{
    public CreateSystemNotificationCommandValidator()
    {
        RuleFor(p => p.SystemNotification.Message).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
