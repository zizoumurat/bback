using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ProductDefinitionFeatures.Commands.CreateProductDefinition;

public sealed record CreateProductDefinitionCommand(ProductDefinitionDto ProductDefinition) : ICommand<CreateProductDefinitionCommandResponse>;