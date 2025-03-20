using FluentValidation;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.ImportCategoryExcel;
public class ImportCategoryExcelCommandValidator : AbstractValidator<ImportCategoryExcelCommand>
{
    public ImportCategoryExcelCommandValidator()
    {
        RuleFor(p => p.excelFile).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
