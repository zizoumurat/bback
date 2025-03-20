using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.SetNonconformity;
public class SetNonconformityCommandHandler : ICommandHandler<SetNonconformityCommand, SetNonconformityCommandResponse>
{
    private readonly IOrderService _orderService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public SetNonconformityCommandHandler(ILocalizationService localizationService, IOrderService orderService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _orderService = orderService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<SetNonconformityCommandResponse> Handle(SetNonconformityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _orderService.SetNonconformityAsync(request.Model);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("Uygunsuzluk Bildirimi Başarıyla Paylaşıldı"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
