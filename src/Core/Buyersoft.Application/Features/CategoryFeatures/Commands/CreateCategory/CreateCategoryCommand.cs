using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.CreateCategory;

public sealed record CreateCategoryCommand(CategoryDto Category) : ICommand<CreateCategoryCommandResponse>;