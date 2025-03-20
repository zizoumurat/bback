using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.OrderPreparationFeatures.Commands.CreateOrder;
public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IOrderPreparationService _orderPreparationService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreateOrderCommandHandler(ILocalizationService localizationService, IOrderPreparationService orderPreparationService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _orderPreparationService = orderPreparationService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _orderPreparationService.CreateOrder(request.Order);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("OrderCreated"));
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
