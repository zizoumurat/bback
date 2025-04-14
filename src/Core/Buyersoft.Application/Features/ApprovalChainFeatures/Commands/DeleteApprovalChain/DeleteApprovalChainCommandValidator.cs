using FluentValidation;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Commands.DeleteApprovalChain;

public class DeleteApprovalChainCommandValidator : AbstractValidator<DeleteApprovalChainCommand>
{
    public DeleteApprovalChainCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
