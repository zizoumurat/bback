using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Queries.GetAllServiceDefinitions;

public sealed record GetAllServiceDefinitionsQueryResponse(PaginatedList<ServiceDefinitionDto> result);
