using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.CompanyFeatures.Queries.GetCurrentCompany;
public sealed record GetCurrentCompanyQuery() : IQuery<GetCurrentCompanyQueryResponse>;
