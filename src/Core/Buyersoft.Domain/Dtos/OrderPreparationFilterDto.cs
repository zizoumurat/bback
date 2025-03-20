using Buyersoft.Domain.Enums;

namespace Buyersoft.Domain.Dtos;

public sealed record OrderPreparationFilterDto(int? MainCategoryId, int? SubCategoryId, int? RequestGroupId, OrderStatusEnum status);


