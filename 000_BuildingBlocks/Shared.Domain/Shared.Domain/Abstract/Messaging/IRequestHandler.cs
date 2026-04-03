namespace Shared.Application.Abstractions.Messaging
{
    public interface IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public delegate Task<TResponse> MessageHandlerDelegate<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken);
}
