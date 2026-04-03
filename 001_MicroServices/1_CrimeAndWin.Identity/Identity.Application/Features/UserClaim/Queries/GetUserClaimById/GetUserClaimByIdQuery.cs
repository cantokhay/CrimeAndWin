using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserClaim.Queries.GetUserClaimById
{
    public record GetUserClaimByIdQuery(Guid id) : IRequest<ResultUserClaimDTO>;
}


