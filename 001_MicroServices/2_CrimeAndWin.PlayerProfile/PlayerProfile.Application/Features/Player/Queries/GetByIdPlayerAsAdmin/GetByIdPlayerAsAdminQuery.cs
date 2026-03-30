using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;

namespace PlayerProfile.Application.Features.Player.Queries.GetByIdPlayerAsAdmin
{
    public sealed record GetPlayerByIdAsAdminQuery(Guid Id) : IRequest<AdminResultPlayerDTO?>;
}
