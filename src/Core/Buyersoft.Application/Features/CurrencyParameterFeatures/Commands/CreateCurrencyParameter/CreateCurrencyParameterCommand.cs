using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.CreateCurrencyParameter;

public sealed record CreateCurrencyParameterCommand(CurrencyParameterDto CurrencyParameter) : ICommand<CreateCurrencyParameterCommandResponse>;