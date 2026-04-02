using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserClaim.Queries.GetAllUserClaims
{
    public record GetAllUserClaimsQuery() : IRequest<List<ResultUserClaimDTO>>;
}

