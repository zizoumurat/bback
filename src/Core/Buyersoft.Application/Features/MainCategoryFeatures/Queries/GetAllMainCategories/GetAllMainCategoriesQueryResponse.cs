using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Queries.GetAllMainCategories;

public sealed record GetAllMainCategoriesQueryResponse(PaginatedList<MainCategoryListDto> result);
