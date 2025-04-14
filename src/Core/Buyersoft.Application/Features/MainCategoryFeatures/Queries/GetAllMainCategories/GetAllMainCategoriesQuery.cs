using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.MainCategoryFeatures.Queries.GetAllMainCategories;
public sealed record GetAllMainCategoriesQuery(MainCategoryFilterDto filter, PageRequest pagination) : IQuery<GetAllMainCategoriesQueryResponse>;
