using Identity.Application.DTOs.UserDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Commands.CreateAppUser
{
    public record CreateAppUserCommand(CreateAppUserDTO createAppUserDTO) : IRequest<Guid>;
}


