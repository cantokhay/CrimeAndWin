using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserClaim.Queries.GetAllUserClaims
{
    public record GetAllUserClaimsQuery() : IRequest<List<ResultUserClaimDTO>>;
}


