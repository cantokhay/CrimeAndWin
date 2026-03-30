using Identity.Application.DTOs.UserTokenDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserToken.Commands.CreateUserToken
{
    public record CreateUserTokenCommand(CreateUserTokenDTO createUserTokenDTO) : IRequest<Guid>;
}
