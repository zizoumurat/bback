using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.CompanySubCategoryFeatures.Queries.GetAllCompanySubCategories;
public sealed record GetAllCompanySubCategoriesQuery(int MainCategoryId) : IQuery<GetAllCompanySubCategoriesQueryResponse>;
