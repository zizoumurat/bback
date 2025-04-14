using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.DepartmentFeatures.Commands.UpdateDepartment;
public class UpdateDepartmentCommandHandler : ICommandHandler<UpdateDepartmentCommand, UpdateDepartmentCommandResponse>
{
    private readonly IDepartmentService _departmentService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public UpdateDepartmentCommandHandler(ILocalizationService localizationService, IDepartmentService departmentService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _departmentService = departmentService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<UpdateDepartmentCommandResponse> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _departmentService.UpdateAsync(companyId, request.Department);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("DepartmentUpdated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
