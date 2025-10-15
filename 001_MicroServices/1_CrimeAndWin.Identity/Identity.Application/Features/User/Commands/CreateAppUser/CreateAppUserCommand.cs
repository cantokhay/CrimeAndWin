using Identity.Application.DTOs.UserDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.User.Commands.CreateAppUser
{
    public record CreateAppUserCommand(CreateAppUserDTO createAppUserDTO) : IRequest<Guid>;
}
