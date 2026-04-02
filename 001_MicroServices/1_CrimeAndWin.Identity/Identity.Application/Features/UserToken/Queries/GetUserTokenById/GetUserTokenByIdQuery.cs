using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserToken.Queries.GetUserTokenById
{
    public record GetUserTokenByIdQuery(Guid id) : IRequest<ResultUserTokenDTO>;
}

