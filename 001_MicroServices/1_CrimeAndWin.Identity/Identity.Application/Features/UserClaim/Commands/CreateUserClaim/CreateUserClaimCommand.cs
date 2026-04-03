using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserClaim.Commands.CreateUserClaim
{
    public record CreateUserClaimCommand(CreateUserClaimDTO createUserClaimDTO) : IRequest<Guid>;
}


