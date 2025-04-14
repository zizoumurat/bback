using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.CompanyFeatures.Queries.GetSupplierList;

public sealed record GetSupplierListQuery
    (SupplierFilterDto filter, PageRequest pagination) : IQuery<GetSupplierListQueryResponse>;
