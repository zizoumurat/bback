using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetTemplateById;
public sealed record GetTemplateByIdQuery(int id) : IQuery<GetTemplateByIdResponse>;
