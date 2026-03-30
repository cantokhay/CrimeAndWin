using Identity.Application.DTOs.UserTokenDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserToken.Commands.UpdateUserToken
{
    public record UpdateUserTokenCommand(UpdateUserTokenDTO updateUserTokenDTO) : IRequest<bool>;
}
