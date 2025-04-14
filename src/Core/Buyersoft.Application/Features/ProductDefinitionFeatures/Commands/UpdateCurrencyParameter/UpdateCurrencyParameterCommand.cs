using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.UpdateProductDefinition;

public sealed record UpdateProductDefinitionCommand(ProductDefinitionDto ProductDefinition) : ICommand<UpdateProductDefinitionCommandResponse>;