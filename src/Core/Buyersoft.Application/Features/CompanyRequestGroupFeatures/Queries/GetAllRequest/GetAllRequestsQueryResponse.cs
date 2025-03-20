using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CompanyRequestGroupFeatures.Queries.GetAllCompanyRequestGroups;

public sealed record GetAllCompanyRequestGroupsQueryResponse(IList<CompanyRequestGroupListDto> result);
