using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.RefreshToken.Commands.CreateRefreshToken
{
    public record CreateRefreshTokenCommand(CreateRefreshTokenDTO createRefreshTokenDTO) : IRequest<Guid>;
}
