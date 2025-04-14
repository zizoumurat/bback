using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.UpdateCurrencyParameter;

public sealed record UpdateCurrencyParameterCommand(CurrencyParameterDto CurrencyParameter) : ICommand<UpdateCurrencyParameterCommandResponse>;