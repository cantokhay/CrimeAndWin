using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserToken.Commands.UpdateUserToken
{
    public record UpdateUserTokenCommand(UpdateUserTokenDTO updateUserTokenDTO) : IRequest<bool>;
}


