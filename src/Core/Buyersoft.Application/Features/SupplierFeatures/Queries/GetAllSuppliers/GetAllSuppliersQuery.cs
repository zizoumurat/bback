using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllSuppliers;

public sealed record GetAllSuppliersQuery
    (SupplierFilterDto filter, PageRequest pagination) : IQuery<GetAllSuppliersQueryResponse>;
