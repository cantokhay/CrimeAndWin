using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.RefreshToken.Queries.GetRefreshTokenById
{
    public record GetRefreshTokenByIdQuery(Guid id) : IRequest<ResultRefreshTokenDTO>;
}
