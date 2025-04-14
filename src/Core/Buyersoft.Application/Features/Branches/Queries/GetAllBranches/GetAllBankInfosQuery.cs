using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllBranches;

public sealed record GetAllBranchesQuery
    (BranchFilterDto filter, PageRequest pagination) : IQuery<GetAllBranchesQueryResponse>;
