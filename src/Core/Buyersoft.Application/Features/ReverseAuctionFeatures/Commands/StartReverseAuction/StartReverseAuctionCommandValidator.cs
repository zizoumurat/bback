using FluentValidation;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Commands.StartReverseAuction;
public class StartReverseAuctionCommandValidator : AbstractValidator<StartReverseAuctionCommand>
{
    public StartReverseAuctionCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
