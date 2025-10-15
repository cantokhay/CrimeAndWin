using Identity.Application.DTOs.UserClaimDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserClaim.Queries.GetAllUserClaims
{
    public record GetAllUserClaimsQuery() : IRequest<List<ResultUserClaimDTO>>;
}
