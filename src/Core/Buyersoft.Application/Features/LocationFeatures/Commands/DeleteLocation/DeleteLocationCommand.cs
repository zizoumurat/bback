using Buyersoft.Application.Features.LocationFeatures.Commands.DeleteLocation;
using Buyersoft.Application.Messaging;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.DeleteLocation;
public sealed record DeleteLocationCommand(int Id) : ICommand<DeleteLocationCommandResponse>;
