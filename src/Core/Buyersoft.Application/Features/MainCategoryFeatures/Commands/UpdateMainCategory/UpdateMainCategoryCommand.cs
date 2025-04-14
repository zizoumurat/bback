using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Commands.UpdateMainCategory;

public sealed record UpdateMainCategoryCommand(MainCategoryDto MainCategory) : ICommand<UpdateMainCategoryCommandResponse>;