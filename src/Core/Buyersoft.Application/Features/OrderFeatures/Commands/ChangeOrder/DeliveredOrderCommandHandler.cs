using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.ChangeOrderStatus;
public class ChangeOrderStatusCommandHandler : ICommandHandler<ChangeOrderStatusCommand, ChangeOrderStatusCommandResponse>
{
    private readonly IOrderService _orderService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public ChangeOrderStatusCommandHandler(ILocalizationService localizationService, IOrderService orderService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _orderService = orderService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<ChangeOrderStatusCommandResponse> Handle(ChangeOrderStatusCommand request, CancellationToken CancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _orderService.ChangeOrderStatusAsync(request.Model);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("Sipariş Statüsü Güncellendi"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
