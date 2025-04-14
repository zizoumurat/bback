using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Queries.GetAllProductDefinitions;

public sealed record GetAllProductDefinitionsQueryResponse(PaginatedList<ProductDefinitionDto> result);
