using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OrderFeatures.Commands.CancelOrder;
public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, CancelOrderCommandResponse>
{
    private readonly IOrderService _orderService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;
    
    public CancelOrderCommandHandler(ILocalizationService localizationService, IOrderService orderService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _orderService = orderService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CancelOrderCommandResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _orderService.CancelOrderAsync(request.Model);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("Sipariş Başarıyla İptal Edildi"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
