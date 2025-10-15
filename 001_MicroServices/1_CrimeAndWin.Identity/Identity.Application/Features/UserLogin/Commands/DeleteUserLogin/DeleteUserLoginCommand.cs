using MediatR;

namespace Identity.Application.Features.UserLogin.Commands.DeleteUserLogin
{
    public record DeleteUserLoginCommand(Guid id) : IRequest<bool>;
}
