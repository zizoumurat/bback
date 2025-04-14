using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites.Base;
using MediatR;

namespace Buyersoft.Application.Features.CommonFeatures.Queries.GetSelectListItemsQuery;
public sealed record GetSelectListItemsQuery<T>(Dictionary<string, string> Filters) : IQuery<List<SelectListItemDto>> where T : SelectableEntity;
