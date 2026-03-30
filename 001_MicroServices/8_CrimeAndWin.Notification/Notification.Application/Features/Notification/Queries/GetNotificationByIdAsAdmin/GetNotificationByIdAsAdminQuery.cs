using MediatR;
using Notification.Application.DTOs.Admin;

namespace Notification.Application.Features.Notification.Queries.GetNotificationByIdAsAdmin
{
    public sealed record GetNotificationByIdAsAdminQuery(Guid id) : IRequest<AdminResultNotificationDTO?>;
}
