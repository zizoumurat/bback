using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ApprovalChainFeatures.Queries.GetAllApprovalChains;

public sealed record GetAllApprovalChainsQueryResponse(PaginatedList<ApprovalChainListDto> result);
