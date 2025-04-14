using AutoMapper;
using Buyersoft.Application.Messaging;
using Buyersoft.Application.Services;
using Buyersoft.Domain.UnitOfWorks;

namespace Buyersoft.Application.Features.PaymentListFeatures.Commands.CreatePaymentList;
public class CreatePaymentListCommandHandler : ICommandHandler<CreatePaymentListCommand, CreatePaymentListCommandResponse>
{
    private readonly IPaymentListService _paymentListService;
    private readonly ILocalizationService _localizationService;
    private readonly ITokenService _tokenService;
    private readonly ITransactionManager _transactionManager;

    public CreatePaymentListCommandHandler(ILocalizationService localizationService, IPaymentListService PaymentListService, ITokenService tokenService, ITransactionManager transactionManager)
    {
        _localizationService = localizationService;
        _paymentListService = PaymentListService;
        _tokenService = tokenService;
        _transactionManager = transactionManager;
    }

    public async Task<CreatePaymentListCommandResponse> Handle(CreatePaymentListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int companyId = _tokenService.GetCompanyIdByToken();
            
            await _transactionManager.BeginTransactionAsync();

            await _paymentListService.AddAsync(companyId, request.PaymentList);

            await _transactionManager.CommitAsync();

            return new(_localizationService.GetLocalizedString("PaymentListCreated"));
        }
        catch (Exception)
        {
            await _transactionManager.RollbackAsync();

            throw;
        }
    }
}
