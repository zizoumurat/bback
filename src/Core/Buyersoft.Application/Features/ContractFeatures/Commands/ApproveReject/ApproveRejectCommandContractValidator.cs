using FluentValidation;

namespace Buyersoft.Application.Features.ContractFeatures.Commands.ApproveReject;
public class ApproveRejectCommandContractValidator : AbstractValidator<ApproveRejectContractCommand>
{
    public ApproveRejectCommandContractValidator()
    {
        RuleFor(p => p.Request.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Request.Status).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
