using Identity.Application.DTOs.UserClaimDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserClaim.Queries.GetUserClaimById
{
    public record GetUserClaimByIdQuery(Guid id) : IRequest<ResultUserClaimDTO>;
}
