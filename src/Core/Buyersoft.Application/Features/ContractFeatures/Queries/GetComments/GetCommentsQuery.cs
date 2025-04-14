using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetComments
    ;

public sealed record GetCommentsQuery(int contractId) : IQuery<GetCommentsQueryResponse>;
