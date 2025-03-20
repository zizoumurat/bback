using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CompanyFeatures.Queries.GetSupplierList;

public sealed record GetSupplierListQueryResponse(PaginatedList<SupplierPortfolioDto> result);
