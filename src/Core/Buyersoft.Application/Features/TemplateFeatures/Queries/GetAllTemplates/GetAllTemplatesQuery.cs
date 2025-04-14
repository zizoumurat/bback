using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllTemplates;
public sealed record GetAllTemplatesQuery(TemplateFilterDto filter, PageRequest pagination) : IQuery<GetAllTemplatesQueryResponse>;
