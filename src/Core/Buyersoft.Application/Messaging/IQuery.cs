using MediatR;

namespace Buyersoft.Application.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}