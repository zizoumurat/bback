using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CommonFeatures.Queries.GetSelectListItemsQuery;
public sealed record GetSelectListItemsQueryResponse(List<SelectListItemDto> result);
