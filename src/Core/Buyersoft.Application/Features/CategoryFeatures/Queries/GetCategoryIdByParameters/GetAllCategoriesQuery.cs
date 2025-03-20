using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CategoryFeatures.Queries.GetCategoryIdByParameters;
public sealed record GetCategoryIdByParametersQuery(CategoryGroupFilter filter) : IQuery<GetCategoryIdByParametersQueryResponse>;
