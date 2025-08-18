using Identity.Application.Auth.DTOs;
using MediatR;

namespace Identity.Application.Auth.Commands.Login
{
    public sealed class LoginCommand : IRequest<AuthResultDTO>
    {
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
