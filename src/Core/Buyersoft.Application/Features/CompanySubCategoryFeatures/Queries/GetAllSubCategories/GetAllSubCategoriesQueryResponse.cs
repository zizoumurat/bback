using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CompanySubCategoryFeatures.Queries.GetAllCompanySubCategories;

public sealed record GetAllCompanySubCategoriesQueryResponse(IList<CompanySubCategoryListDto> result);
