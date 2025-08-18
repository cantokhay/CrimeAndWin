using MediatR;
using PlayerProfile.Application.Features.Player.DTOs;

namespace PlayerProfile.Application.Features.Player.Queries
{
    public sealed record GetByIdPlayerQuery(Guid Id) : IRequest<ResultPlayerDTO?>;
}
