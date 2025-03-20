using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites;
using Buyersoft.Domain.Entitites.Base;
using Buyersoft.Domain.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Buyersoft.Application.Features.CommonFeatures.Queries.GetSelectListItemsQuery;
public class GetSelectListItemsQueryHandler<T> : IQueryHandler<GetSelectListItemsQuery<T>, List<SelectListItemDto>>
    where T : SelectableEntity
{
    private readonly IQueryRepository<T> _repository;

    public GetSelectListItemsQueryHandler(IQueryRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<List<SelectListItemDto>> Handle(GetSelectListItemsQuery<T> request, CancellationToken cancellationToken)
    {
        var query = _repository.GetList(x => true);

        if (request.Filters != null)
        {
            foreach (var filter in request.Filters)
            {
                query = query.Where($"{filter.Key} == @0", filter.Value);
            }
        }

        return await query.Select(x=> new SelectListItemDto(x.Id, x.Name)).ToListAsync();

    }
}