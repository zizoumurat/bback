using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequests;

public sealed record GetAllRequestsQueryResponse(PaginatedList<RequestListDto> result);
