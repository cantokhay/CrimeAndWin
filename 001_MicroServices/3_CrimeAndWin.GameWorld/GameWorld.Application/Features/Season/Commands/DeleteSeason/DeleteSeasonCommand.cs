using Shared.Application.Abstractions.Messaging;

namespace GameWorld.Application.Features.Season.Commands.DeleteSeason
{
    public sealed record DeleteSeasonCommand(Guid Id) : IRequest<bool>;
}



