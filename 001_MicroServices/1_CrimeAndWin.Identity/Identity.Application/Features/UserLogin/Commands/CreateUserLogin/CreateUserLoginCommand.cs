using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserLogin.Commands.CreateUserLogin
{
    public record CreateUserLoginCommand(CreateUserLoginDTO createUserLoginDTO) : IRequest<Guid>;
}

