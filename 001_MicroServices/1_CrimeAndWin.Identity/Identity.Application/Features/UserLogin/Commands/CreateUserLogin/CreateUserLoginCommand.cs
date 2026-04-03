using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserLogin.Commands.CreateUserLogin
{
    public record CreateUserLoginCommand(CreateUserLoginDTO createUserLoginDTO) : IRequest<Guid>;
}


