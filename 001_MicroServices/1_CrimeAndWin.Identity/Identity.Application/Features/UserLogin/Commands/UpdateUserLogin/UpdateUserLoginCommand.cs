using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserLogin.Commands.UpdateUserLogin
{
    public record UpdateUserLoginCommand(UpdateUserLoginDTO updateUserLoginDTO) : IRequest<bool>;
}

