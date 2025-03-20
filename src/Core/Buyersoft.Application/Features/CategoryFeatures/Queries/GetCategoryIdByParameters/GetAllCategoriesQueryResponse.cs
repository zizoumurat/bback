using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CategoryFeatures.Queries.GetCategoryIdByParameters;

public sealed record GetCategoryIdByParametersQueryResponse(CategoryListDto result);
