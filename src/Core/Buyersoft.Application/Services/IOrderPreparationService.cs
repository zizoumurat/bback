using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IOrderPreparationService
{
    Task AddAsync(int companyId, int RequestId, int OfferId);

    Task CreateOrder(OrderCreateDto Model);

    Task<PaginatedList<OrderPreparationListDto>> GetAllAsync(int companyId, OrderPreparationFilterDto filter, PageRequest pagination);
}
