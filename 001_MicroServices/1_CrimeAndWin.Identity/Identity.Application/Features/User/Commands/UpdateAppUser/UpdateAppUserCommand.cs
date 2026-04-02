using Identity.Application.DTOs.UserDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.User.Commands.UpdateAppUser
{
    public record UpdateAppUserCommand(UpdateAppUserDTO updateAppUserDTO) : IRequest<bool>;
}

