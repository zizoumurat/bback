using FluentValidation;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.DeliveredOrder;
public class DeliveredOrderCommandValidator : AbstractValidator<DeliveredOrderCommand>
{
    public DeliveredOrderCommandValidator()
    {
    }
}
