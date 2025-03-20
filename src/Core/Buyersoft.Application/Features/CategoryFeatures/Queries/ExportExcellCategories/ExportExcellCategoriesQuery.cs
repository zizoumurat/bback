using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.CategoryFeatures.Queries.ExportExcellCategories;
public sealed record ExportExcellCategoriesQuery(CategoryFilterDto filter) : IQuery<ExportExcellCategoriesQueryResponse>;
