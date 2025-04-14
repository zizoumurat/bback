using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllContracts
    ;

public sealed record GetAllContractsQuery(ContractFilterDto filter, PageRequest pagination) : IQuery<GetAllContractsQueryResponse>;
