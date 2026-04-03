using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.RefreshToken.Queries.GetRefreshTokenById
{
    public record GetRefreshTokenByIdQuery(Guid id) : IRequest<ResultRefreshTokenDTO>;
}


