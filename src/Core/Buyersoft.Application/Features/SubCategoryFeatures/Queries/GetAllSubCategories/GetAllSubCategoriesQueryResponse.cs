using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SubCategoryFeatures.Queries.GetAllSubCategories;

public sealed record GetAllSubCategoriesQueryResponse(IList<SubCategoryListDto> result);
