using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.RequestFeatures.Queries.GetAllRequestById;
public sealed record GetAllRequestByIdQuery(int Id) : IQuery<GetAllRequestByIdQueryResponse>;
