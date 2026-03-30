using MediatR;

namespace Identity.Application.Features.UserClaim.Commands.DeleteUserClaim
{
    public record DeleteUserClaimCommand(Guid Id) : IRequest<bool>;
}
