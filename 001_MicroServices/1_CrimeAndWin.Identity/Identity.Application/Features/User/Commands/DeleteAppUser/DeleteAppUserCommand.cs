using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Commands.DeleteAppUser
{
    public record DeleteAppUserCommand(Guid id) : IRequest<bool>;
}


