using Buyersoft.Application.Features.Pagination;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Pagination;

namespace Buyersoft.Application.Services;
public interface IContractService
{
    Task<PaginatedList<ContractListDto>> GetAllAsync(int companyId, int userId, ContractFilterDto filter, PageRequest pagination);
    Task UploadContractFileAsync(int userId, int companyId, UploadContractFileDto model);
    Task ApproveRejectContract(int userId, int companyId, ApproveRejectContractDto model);

    Task<List<CommentListDto>> GetCommentList(int contractId);
    Task AddComment(int userId, string userName, AddCommentDto model);

}
