using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.ImportCategoryExcel;

public sealed record ImportCategoryExcelCommand(IFormFile excelFile) : ICommand<ImportCategoryExcelCommandResponse>;