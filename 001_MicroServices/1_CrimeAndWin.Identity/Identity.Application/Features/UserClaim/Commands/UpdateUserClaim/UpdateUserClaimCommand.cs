using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserClaim.Commands.UpdateUserClaim
{
    public record UpdateUserClaimCommand(UpdateUserClaimDTO updateUserClaimDTO) : IRequest<bool>;
}


