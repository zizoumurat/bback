using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequestById;

public sealed record GetAllRequestByIdQueryResponse(RequestListDto result);
