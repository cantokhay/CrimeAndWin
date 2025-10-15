using MediatR;

namespace Identity.Application.Features.User.Commands.DeleteAppUser
{
    public record DeleteAppUserCommand(Guid id) : IRequest<bool>;
}
