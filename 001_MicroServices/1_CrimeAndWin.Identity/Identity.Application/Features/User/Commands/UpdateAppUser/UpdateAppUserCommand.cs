using Identity.Application.DTOs.UserDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.User.Commands.UpdateAppUser
{
    public record UpdateAppUserCommand(UpdateAppUserDTO updateAppUserDTO) : IRequest<bool>;
}
