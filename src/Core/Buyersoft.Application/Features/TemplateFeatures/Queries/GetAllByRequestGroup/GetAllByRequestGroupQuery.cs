using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.TemplateFeatures.Queries.GetAllByRequestGroup;
public sealed record GetAllByRequestGroupQuery(int requestGrouopId) : IQuery<GetAllByRequestGroupResponse>;
