using Buyersoft.Application.Features.MainCategoryFeatures.Commands.DeleteMainCategory;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.DeleteMainCategory;
public sealed record DeleteMainCategoryCommand(int Id) : ICommand<DeleteMainCategoryCommandResponse>;
