using FluentValidation;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.ApproveReject;
public class ApproveRejectCommandValidator : AbstractValidator<ApproveRejectCommand>
{
    public ApproveRejectCommandValidator()
    {
        RuleFor(p => p.Request.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Request.Status).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
