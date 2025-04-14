using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.LocationFeatures.Queries.GetAllLocations;

public sealed record GetAllLocationsQueryResponse(PaginatedList<LocationListDto> result);
