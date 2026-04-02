using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserClaim.Commands.CreateUserClaim
{
    public record CreateUserClaimCommand(CreateUserClaimDTO createUserClaimDTO) : IRequest<Guid>;
}

