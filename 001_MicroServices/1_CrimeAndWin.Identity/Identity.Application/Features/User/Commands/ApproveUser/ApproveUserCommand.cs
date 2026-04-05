using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Commands.ApproveUser
{
    public sealed record ApproveUserCommand(Guid UserId) : IRequest<bool>;
}
