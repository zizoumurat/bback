using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.CategoryFeatures.Queries.GetAllCategories;
public sealed record GetAllCategoriesQuery(CategoryFilterDto filter, PageRequest pagination) : IQuery<GetAllCategoriesQueryResponse>;
