using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.RequestGroupFeatures.Queries.GetAllRequestGroups;

public sealed record GetAllRequestGroupsQueryResponse(IList<RequestGroupListDto> result);
