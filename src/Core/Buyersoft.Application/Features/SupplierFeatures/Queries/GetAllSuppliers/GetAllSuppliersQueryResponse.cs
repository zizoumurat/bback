using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.BrancheFeatures.Queries.GetAllSuppliers;

public sealed record GetAllSuppliersQueryResponse(PaginatedList<SupplierListDto> result);
