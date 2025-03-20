using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IOrderService
{
    Task SetNonconformityAsync(SetNonconformityDto Model);
    Task CancelOrderAsync(CancelOrderDto Model);
    Task DeliveredOrderAsync(DeliveredOrderDto Model);

    Task ChangeOrderStatusAsync(ChangeOrderStatusDto Model);


    Task<PaginatedList<OrderPaginationDto>> GetAllAsync(int companyId, int supplierId, OrderPreparationFilterDto filter, PageRequest pagination);
}
