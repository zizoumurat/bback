using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SubCategoryFeatures.Commands.CreateSubCategory;

public sealed record CreateSubCategoryCommand(SubCategoryDto SubCategory) : ICommand<CreateSubCategoryCommandResponse>;