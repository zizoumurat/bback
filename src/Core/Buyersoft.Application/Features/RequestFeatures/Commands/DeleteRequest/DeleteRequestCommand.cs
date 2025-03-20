using Buyersoft.Application.Features.RequestFeatures.Commands.DeleteRequest;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.RequestFeatures.Commands.DeleteRequest;
public sealed record DeleteRequestCommand(int Id) : ICommand<DeleteRequestCommandResponse>;
