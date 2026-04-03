using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.RefreshToken.Commands.UpdateRefreshToken
{
    public record UpdateRefreshTokenCommand(UpdateRefreshTokenDTO updateRefreshTokenDTO) : IRequest<bool>;
}


