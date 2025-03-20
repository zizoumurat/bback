using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IRequestService
{
    Task AddAsync(int companyId, int userId, CreateRequestDto entity, string userName);
    Task UpdateAsync(int companyId, CreateRequestDto entity);
    Task DeleteAsync(int id, int companyId);
    Task<PaginatedList<RequestListDto>> GetAllAsync(int companyId, int userId, RequestFilterDto filter, PageRequest pagination);
    Task<RequestListDto> GetById(int companyId, int Id);
    Task StartBidCollection(StartBidCollectionDto model);
    Task CancelBidCollection(CancelBidCollectionDto model);
    Task AssignManager(int companyId, int userId, AssignManagerDto model);
    Task CreateComprasionTable(int companyId, int requestId, int offerType); // 1 = all, 2 = shortListed
    Task StartApprovalProcess(int companyId, StartApprovalProcessDto model);
    Task ApproveRejectRequest(int userId, ApproveRejectRequestDto model);
}
