using Buyersoft.Application.Messaging;
using Buyersoft.Domain.Dtos;

namespace Buyersoft.Application.Features.LocationFeatures.Commands.UpdateLocation;

public sealed record UpdateLocationCommand(LocationDto Location) : ICommand<UpdateLocationCommandResponse>;