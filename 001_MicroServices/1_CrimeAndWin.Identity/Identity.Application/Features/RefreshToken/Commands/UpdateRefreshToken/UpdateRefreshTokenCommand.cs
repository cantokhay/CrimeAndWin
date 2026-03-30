using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.RefreshToken.Commands.UpdateRefreshToken
{
    public record UpdateRefreshTokenCommand(UpdateRefreshTokenDTO updateRefreshTokenDTO) : IRequest<bool>;
}
