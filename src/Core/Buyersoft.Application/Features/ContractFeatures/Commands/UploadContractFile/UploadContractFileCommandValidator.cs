using FluentValidation;

namespace Buyersoft.Application.Features.ContractFeatures.Commands.UploadContractFile;
public class UploadContractFileCommandValidator : AbstractValidator<UploadContractFileCommand>
{
    public UploadContractFileCommandValidator()
    {
        RuleFor(p => p.Model.File).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
