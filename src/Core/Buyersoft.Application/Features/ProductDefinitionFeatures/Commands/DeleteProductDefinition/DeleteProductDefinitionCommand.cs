﻿using Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.DeleteProductDefinition;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.DeleteProductDefinition;
public sealed record DeleteProductDefinitionCommand(int Id) : ICommand<DeleteProductDefinitionCommandResponse>;
