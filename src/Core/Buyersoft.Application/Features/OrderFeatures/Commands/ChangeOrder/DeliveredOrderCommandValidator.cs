using FluentValidation;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.ChangeOrderStatus;
public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
{
    public ChangeOrderStatusCommandValidator()
    {
    }
}
