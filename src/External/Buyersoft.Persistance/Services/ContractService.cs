using AutoMapper;
using AutoMapper.QueryableExtensions;
using Buyersoft.Application.Features.Pagination;
using Buyersoft.Application.Services;
using Buyersoft.Domain.Dtos;
using Buyersoft.Domain.Entitites.Identity;
using Buyersoft.Domain.Enums;
using Buyersoft.Domain.Pagination;
using Buyersoft.Domain.Repositories.ContractRepositories;
using Buyersoft.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Buyersoft.Persistance.Services;
public class ContractService : IContractService
{
    private readonly IQueryContractRepository _queryContractRepository;
    private readonly IUpdateContractRepository _updateContractRepository;
    private readonly IMapper _mapper;
    private readonly IDocumentService _documentService;
    private readonly ISendNotificationService _sendNotificationService;
    private readonly IUserService _userService;
    private readonly IOrderPreparationService _orderPreparationService;


    public ContractService(IQueryContractRepository queryContractRepository, IMapper mapper, IDocumentService documentService, IUpdateContractRepository updateContractRepository, ISendNotificationService sendNotificationService, IUserService userService, IOrderPreparationService orderPreparationService)
    {
        _queryContractRepository = queryContractRepository;
        _mapper = mapper;
        _documentService = documentService;
        _updateContractRepository = updateContractRepository;
        _sendNotificationService = sendNotificationService;
        _userService = userService;
        _orderPreparationService = orderPreparationService;
    }

    public async Task<PaginatedList<ContractListDto>> GetAllAsync(int companyId, int userId, ContractFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _queryContractRepository.GetList(x => (x.CompanyId == companyId && x.ContractStatus >= Domain.Enums.ContractStatus.Started)
        || x.Request.ManagerId == userId || x.Request.Approvals.Any(a => a.Id == userId && x.ContractStatus == Domain.Enums.ContractStatus.OfferApproved));


        query = query.Include(x => x.Document);
        query = query.AsQueryable();

        var count = await query.CountAsync();
        var items = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize).MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .ProjectTo<ContractListDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

        return new PaginatedList<ContractListDto>(items, count, pagination.Page, pagination.PageSize);
    }



    public async Task UploadContractFileAsync(int userId, int companyId, UploadContractFileDto model)
    {
        var contract = await _queryContractRepository.GetFirstAsync(x => x.Id == model.Id && x.Request.ManagerId == userId || x.CompanyId == companyId)
            .FirstOrDefaultAsync();

        if (contract == null)
        {
            throw new InvalidOperationException(("NotFoundEntity"));
        }

        contract.StartDate = Convert.ToDateTime(model.StartDate);
        contract.ExpirationDate = Convert.ToDateTime(model.ExpirationDate);

        if (contract.DocumentId == null)
        {
            int fileId = await _documentService.UploadDocument(model.File);
            contract.DocumentId = fileId;
        }
        else
        {

            await _documentService.UpdateAsync(model.File, contract.DocumentId.Value);
        }


        contract.ContractStatus = companyId == contract.CompanyId ? Domain.Enums.ContractStatus.OfferApproved : Domain.Enums.ContractStatus.Started;

        _updateContractRepository.Update(contract);
    }

    public async Task ApproveRejectContract(int userId, int companyId, ApproveRejectContractDto model)
    {
        var contract = await _queryContractRepository.GetFirstAsync(x => x.Id == model.Id).Include(x => x.ContractApprovals).FirstOrDefaultAsync();

        bool contractApproved = false;

        if (contract.CompanyId == companyId)
        {
            contract.ContractStatus = model.Status == ApprovalStatus.Approved ? Domain.Enums.ContractStatus.OfferApproved : Domain.Enums.ContractStatus.OfferRejected;
        }
        else
        {
            var approval = contract.ContractApprovals.FirstOrDefault(x => x.UserId == userId);

            if (approval != null)
            {
                approval.Status = model.Status;
            }

            if (contract.ContractApprovals.All(x => x.Status == ApprovalStatus.Approved))
            {
                contract.ContractStatus = ContractStatus.ContractApproved;

                contractApproved = true;
            }

            if (contract.ContractApprovals.Any(x => x.Status == ApprovalStatus.Rejected))
            {
                contract.ContractStatus = ContractStatus.ContractRejected;
            }
        }

        _updateContractRepository.Update(contract);

        if(contractApproved)
        {
            await _orderPreparationService.AddAsync(companyId, contract.RequestId, contract.OfferId);
        }
    }

    public async Task<List<CommentListDto>> GetCommentList(int contractId)
    {
        var list = await _queryContractRepository.GetFirstAsync(x => x.Id == contractId)
            .Include(x => x.ContractComments)
            .SelectMany(x => x.ContractComments.OrderBy(x=>x.CommentDate))
            .ProjectTo<CommentListDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;
    }

    public async Task AddComment(int userId, string userName, AddCommentDto model)
    {
       var user= await _userService.GetCurrentUser(userId);
      
        var contract = await _queryContractRepository.GetFirstAsync(x => x.Id == model.ContractId).Include(x=>x.ContractComments).FirstOrDefaultAsync();

        contract.ContractComments.Add(new Domain.Entitites.ContractComment()
        {
            UserId = userId,
            Comment = model.Comment,
            CommentDate = DateTime.Now
        });

        _updateContractRepository.Update(contract);

        await _sendNotificationService.SendComment(contractId: model.ContractId.ToString(), user: user.Name.ToLower(), message: model.Comment);
    }
}
