using MediatR;
using Notification.Application.DTOs;

namespace Notification.Application.Features.Notification.Queries
{
    public record GetNotificationQuery(Guid PlayerId) : IRequest<List<ResultNotificationDTO>>;
}
