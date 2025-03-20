using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllByRequestGroup;

public sealed record GetAllByRequestGroupResponse(IList<TemplateListDto> result);
