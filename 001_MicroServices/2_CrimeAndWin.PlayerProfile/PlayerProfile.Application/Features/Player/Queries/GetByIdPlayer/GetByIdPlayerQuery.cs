using MediatR;
using PlayerProfile.Application.DTOs.PlayerDTOs;

namespace PlayerProfile.Application.Features.Player.Queries.GetByIdPlayer
{
    public sealed record GetByIdPlayerQuery(Guid Id) : IRequest<ResultPlayerDTO?>;
}
