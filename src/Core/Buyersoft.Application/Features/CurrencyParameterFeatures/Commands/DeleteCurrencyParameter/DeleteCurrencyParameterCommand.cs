﻿using Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.DeleteCurrencyParameter;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.CurrencyParameterFeatures.Commands.DeleteCurrencyParameter;
public sealed record DeleteCurrencyParameterCommand(int Id) : ICommand<DeleteCurrencyParameterCommandResponse>;
