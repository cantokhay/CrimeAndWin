using Mediator;

namespace Identity.Application.Features.UserToken.Commands.DeleteUserToken
{
    public record DeleteUserTokenCommand(Guid id) : IRequest<bool>;
}

