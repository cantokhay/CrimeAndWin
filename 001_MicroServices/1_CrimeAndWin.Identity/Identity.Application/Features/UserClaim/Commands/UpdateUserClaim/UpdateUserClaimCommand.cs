using Identity.Application.DTOs.UserClaimDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.UserClaim.Commands.UpdateUserClaim
{
    public record UpdateUserClaimCommand(UpdateUserClaimDTO updateUserClaimDTO) : IRequest<bool>;
}
