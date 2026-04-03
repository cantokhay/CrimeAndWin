using Microsoft.Extensions.DependencyInjection;

namespace Shared.Application.Abstractions.Messaging
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var handlerType = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
                throw new InvalidOperationException($"Handler not found for {request.GetType().Name}");

            return await (Task<TResponse>)handlerType
                .GetMethod("Handle")!
                .Invoke(handler, new object[] { request, cancellationToken })!;
        }
    }
}
