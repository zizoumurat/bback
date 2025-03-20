using MediatR;

namespace Buyersoft.Application.Messaging;
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}