using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.OrderFeatures.Queries.GetAllOrder;

public sealed record GetAllOrderQueryResponse(PaginatedList<OrderPaginationDto> result);
