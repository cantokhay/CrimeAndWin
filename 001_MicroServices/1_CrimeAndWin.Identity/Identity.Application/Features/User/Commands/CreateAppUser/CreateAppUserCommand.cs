using Identity.Application.DTOs.UserDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.User.Commands.CreateAppUser
{
    public record CreateAppUserCommand(CreateAppUserDTO createAppUserDTO) : IRequest<Guid>;
}

