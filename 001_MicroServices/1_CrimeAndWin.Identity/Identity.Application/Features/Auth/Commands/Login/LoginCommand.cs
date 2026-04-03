using Identity.Application.DTOs.AuthDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.Auth.Commands.Login
{
    public sealed class LoginCommand : IRequest<ResultAuthDTO>
    {
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}


