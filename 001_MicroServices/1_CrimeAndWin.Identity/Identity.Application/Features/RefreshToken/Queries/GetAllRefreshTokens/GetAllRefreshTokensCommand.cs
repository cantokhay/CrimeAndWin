using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.RefreshToken.Queries.GetAllRefreshTokens
{
    public record GetAllRefreshTokensQuery() : IRequest<List<ResultRefreshTokenDTO>>;
}


