using Identity.Application.DTOs.UserDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Commands.UpdateAppUser
{
    public record UpdateAppUserCommand(UpdateAppUserDTO updateAppUserDTO) : IRequest<bool>;
}


