using Buyersoft.Application.Features.CategoryFeatures.Commands.DeleteCategory;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.CategoryFeatures.Commands.DeleteCategory;
public sealed record DeleteCategoryCommand(int Id) : ICommand<DeleteCategoryCommandResponse>;
