using FluentValidation;

namespace Buyersoft.Application.Features.SystemNotificationFeatures.Commands.UpdateSystemNotification;
public class UpdateSystemNotificationCommandValidator : AbstractValidator<UpdateSystemNotificationCommand>
{
    public UpdateSystemNotificationCommandValidator()
    {
        RuleFor(p => p.SystemNotification.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.SystemNotification.Message).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
