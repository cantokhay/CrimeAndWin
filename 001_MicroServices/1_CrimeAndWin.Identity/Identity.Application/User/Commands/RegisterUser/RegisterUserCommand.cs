using Identity.Application.User.DTOs;
using MediatR;

namespace Identity.Application.User.Commands.RegisterUser
{
    public sealed class RegisterUserCommand : IRequest<AppUserDTO>
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
