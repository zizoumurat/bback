using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(CategoryDto Category) : ICommand<UpdateCategoryCommandResponse>;