using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetListByRequestId;
public sealed record GetListByRequestIdQuery(int Id) : IQuery<GetListByRequestIdQueryResponse>;
