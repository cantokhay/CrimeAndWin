using Notification.Application.Mapping;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Notification.Application.DTOs;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Queries.GetAllNotifications
{
    public sealed class GetAllNotificationsHandler(
            IReadRepository<Domain.Entities.Notification> readRepo,
            NotificationMapper mapper)
            : IRequestHandler<GetAllNotificationsQuery, List<ResultNotificationDTO>>
    {
        public async ValueTask<List<ResultNotificationDTO>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await readRepo.Table
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(notifications).ToList();
        }
    }
}


