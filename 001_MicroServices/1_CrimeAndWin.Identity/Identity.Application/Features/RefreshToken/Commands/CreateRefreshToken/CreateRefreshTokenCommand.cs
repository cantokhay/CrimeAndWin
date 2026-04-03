using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.RefreshToken.Commands.CreateRefreshToken
{
    public record CreateRefreshTokenCommand(CreateRefreshTokenDTO createRefreshTokenDTO) : IRequest<Guid>;
}


