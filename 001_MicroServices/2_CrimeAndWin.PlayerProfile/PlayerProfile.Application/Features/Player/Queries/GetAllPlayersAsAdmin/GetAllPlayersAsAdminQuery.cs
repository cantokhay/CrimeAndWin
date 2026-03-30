using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;

namespace PlayerProfile.Application.Features.Player.Queries.GetAllPlayersAsAdmin
{
    public sealed record GetAllPlayersAsAdminQuery() : IRequest<List<AdminResultPlayerDTO>>;
}
