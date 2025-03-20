using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Queries.GetAllApprovalChains;

public sealed record GetAllApprovalChainsQuery
    (ApprovalChainFilterDto filter, PageRequest pagination) : IQuery<GetAllApprovalChainsQueryResponse>;
