using Mediator;

namespace PlayerProfile.Application.Features.Player.Commands.UpdateAvatar
{
    public sealed record UpdateAvatarCommand(Guid PlayerId, string AvatarKey) : IRequest<Unit>;
}

