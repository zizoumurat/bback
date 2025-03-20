using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllTemplates;

public sealed record GetAllTemplatesQueryResponse(PaginatedList<TemplateListDto> result);
