using Identity.Application.DTOs.AuthDTOs;
using MediatR;

namespace Identity.Application.Features.Auth.Commands.Login
{
    public sealed class LoginCommand : IRequest<ResultAuthDTO>
    {
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
