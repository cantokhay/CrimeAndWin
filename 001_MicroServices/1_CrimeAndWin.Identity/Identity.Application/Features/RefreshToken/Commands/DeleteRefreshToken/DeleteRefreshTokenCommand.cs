using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.RefreshToken.Commands.DeleteRefreshToken
{
    public record DeleteRefreshTokenCommand(Guid id) : IRequest<bool>;
}


