using Identity.Application.DTOs.UserTokenDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserToken.Queries.GetAllUserTokens
{
    public record GetAllUserTokensQuery() : IRequest<List<ResultUserTokenDTO>>;
}
