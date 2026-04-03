using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserToken.Queries.GetAllUserTokens
{
    public record GetAllUserTokensQuery() : IRequest<List<ResultUserTokenDTO>>;
}


