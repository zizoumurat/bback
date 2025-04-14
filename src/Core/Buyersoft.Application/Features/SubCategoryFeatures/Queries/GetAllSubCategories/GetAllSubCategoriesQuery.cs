using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.SubCategoryFeatures.Queries.GetAllSubCategories;
public sealed record GetAllSubCategoriesQuery(int MainCategoryId) : IQuery<GetAllSubCategoriesQueryResponse>;
