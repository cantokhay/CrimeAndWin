using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.RefreshToken.Queries.GetAllRefreshTokens
{
    public record GetAllRefreshTokensQuery() : IRequest<List<ResultRefreshTokenDTO>>;
}

