using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.LocationFeatures.Queries.GetAllLocations;
public sealed record GetAllLocationsQuery(LocationFilterDto filter, PageRequest pagination) : IQuery<GetAllLocationsQueryResponse>;
