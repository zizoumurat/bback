using FluentValidation;

namespace Buyersoft.Application.Features.OrderPreparationFeatures.Commands.CreateOrder;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(p => p.Order.OrderItems).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Order.OrderPreparationId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
