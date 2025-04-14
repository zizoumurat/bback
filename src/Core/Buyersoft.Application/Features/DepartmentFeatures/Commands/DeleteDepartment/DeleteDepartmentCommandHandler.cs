using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.DepartmentFeatures.Commands.DeleteDepartment;

public class DeleteDepartmentCommandHandler : ICommandHandler<DeleteDepartmentCommand, DeleteDepartmentCommandResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public DeleteDepartmentCommandHandler(ILocalizationService localizationService, IDepartmentService departmentService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _departmentService = departmentService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<DeleteDepartmentCommandResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _departmentService.DeleteAsync(request.Id, companyId);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("DepartmentDeleted"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
