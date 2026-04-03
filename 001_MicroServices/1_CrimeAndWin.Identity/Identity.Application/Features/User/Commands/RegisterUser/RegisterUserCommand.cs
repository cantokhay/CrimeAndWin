using Identity.Application.DTOs.UserDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Commands.RegisterUser
{
    public sealed class RegisterUserCommand : IRequest<AppUserDTO>
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}


