using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.SupplierFeatures.Queries.GetCompanyList;

public sealed record GetCompanyListQuery(CompanyFilterDto filter, PageRequest pagination) : IQuery<GetCompanyListQueryResponse>;
