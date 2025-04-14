using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllBranches;

public sealed record GetAllBranchesQueryResponse(PaginatedList<BranchListDto> result);
