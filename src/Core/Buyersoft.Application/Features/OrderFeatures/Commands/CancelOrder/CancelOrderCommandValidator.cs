using FluentValidation;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.CancelOrder;
public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
    }
}
