using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.CreateMainCategory;

public sealed record CreateMainCategoryCommand(MainCategoryDto MainCategory) : ICommand<CreateMainCategoryCommandResponse>;