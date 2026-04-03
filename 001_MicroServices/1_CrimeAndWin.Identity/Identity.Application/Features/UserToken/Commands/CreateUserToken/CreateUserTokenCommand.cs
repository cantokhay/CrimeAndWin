using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserToken.Commands.CreateUserToken
{
    public record CreateUserTokenCommand(CreateUserTokenDTO createUserTokenDTO) : IRequest<Guid>;
}


