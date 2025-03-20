using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.ServiceDefinitionFeatures.Commands.DeleteServiceDefinition;
public sealed record DeleteServiceDefinitionCommand(int Id) : ICommand<DeleteServiceDefinitionCommandResponse>;
