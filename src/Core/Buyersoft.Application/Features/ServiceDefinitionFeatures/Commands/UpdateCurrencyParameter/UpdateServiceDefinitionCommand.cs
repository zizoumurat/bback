using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.UpdateServiceDefinition;

public sealed record UpdateServiceDefinitionCommand(ServiceDefinitionDto ServiceDefinition) : ICommand<UpdateServiceDefinitionCommandResponse>;