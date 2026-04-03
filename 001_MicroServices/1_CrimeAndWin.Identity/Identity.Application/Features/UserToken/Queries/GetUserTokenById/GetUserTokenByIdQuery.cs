using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserToken.Queries.GetUserTokenById
{
    public record GetUserTokenByIdQuery(Guid id) : IRequest<ResultUserTokenDTO>;
}


