using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllContracts;

public sealed record GetAllContractsQueryResponse(PaginatedList<ContractListDto> result);
