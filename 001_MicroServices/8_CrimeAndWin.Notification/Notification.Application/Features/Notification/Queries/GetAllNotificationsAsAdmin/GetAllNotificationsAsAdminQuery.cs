using MediatR;
using Notification.Application.DTOs.Admin;

namespace Notification.Application.Features.Notification.Queries.GetAllNotificationsAsAdmin
{
    public sealed record GetAllNotificationsAsAdminQuery() : IRequest<List<AdminResultNotificationDTO>>;
}
