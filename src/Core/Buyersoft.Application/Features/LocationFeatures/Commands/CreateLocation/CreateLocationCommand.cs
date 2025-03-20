using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.CreateLocation;

public sealed record CreateLocationCommand(LocationDto Location) : ICommand<CreateLocationCommandResponse>;