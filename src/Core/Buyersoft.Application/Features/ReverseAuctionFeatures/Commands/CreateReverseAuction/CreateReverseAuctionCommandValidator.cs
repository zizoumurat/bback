using FluentValidation;

namespace Buyersoft.Application.Features.ReverseAuctionFeatures.Commands.CreateReverseAuction;
public class CreateReverseAuctionCommandValidator : AbstractValidator<CreateReverseAuctionCommand>
{
    public CreateReverseAuctionCommandValidator()
    {
        RuleFor(p => p.Model.RequestId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Model.StartTime).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Model.EndTime).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Model.OfferIdList).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
