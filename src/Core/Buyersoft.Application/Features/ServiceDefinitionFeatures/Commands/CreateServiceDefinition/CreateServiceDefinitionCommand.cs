using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.CreateServiceDefinition;

public sealed record CreateServiceDefinitionCommand(ServiceDefinitionDto ServiceDefinition) : ICommand<CreateServiceDefinitionCommandResponse>;