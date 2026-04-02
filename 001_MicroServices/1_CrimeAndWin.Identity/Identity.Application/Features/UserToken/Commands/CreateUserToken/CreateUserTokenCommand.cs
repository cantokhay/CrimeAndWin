using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserToken.Commands.CreateUserToken
{
    public record CreateUserTokenCommand(CreateUserTokenDTO createUserTokenDTO) : IRequest<Guid>;
}

