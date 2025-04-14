using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Features.OrderFeatures.Queries.GetAllOrder;
public sealed record GetAllOrderQuery(OrderPreparationFilterDto filter, PageRequest pagination) : IQuery<GetAllOrderQueryResponse>;
