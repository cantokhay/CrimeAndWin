using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserLogin.Commands.UpdateUserLogin
{
    public record UpdateUserLoginCommand(UpdateUserLoginDTO updateUserLoginDTO) : IRequest<bool>;
}


