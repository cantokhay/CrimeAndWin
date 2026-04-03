using Shared.Application.Abstractions.Messaging;
using PlayerProfile.Application.DTOs.PlayerDTOs;

namespace PlayerProfile.Application.Features.Player.Queries.GetAllPlayer
{
    public sealed record GetAllPlayersQuery() : IRequest<List<ResultPlayerDTO>>;
}


