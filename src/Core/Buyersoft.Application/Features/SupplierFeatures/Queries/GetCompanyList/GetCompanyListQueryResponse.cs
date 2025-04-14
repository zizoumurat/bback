using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.SupplierFeatures.Queries.GetCompanyList;

public sealed record GetCompanyListQueryResponse(PaginatedList<SupplierPortfolioDto> result);
