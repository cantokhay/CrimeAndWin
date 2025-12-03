using MediatR;
using Notification.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Notification.Application.Features.Notification.Commands.AdminCreateNotification
{
    public sealed class AdminCreateNotificationCommandHandler
            : IRequestHandler<AdminCreateNotificationCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Notification> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateNotificationCommandHandler(
            IWriteRepository<Domain.Entities.Notification> write,
            IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var d = request.createNotificationDTO;

            var entity = new Domain.Entities.Notification
            {
                Id = Guid.NewGuid(),
                PlayerId = d.PlayerId,
                Content = new NotificationContent(d.Title, d.Message, d.Type),
                CreatedAtUtc = _time.UtcNow,
                IsDeleted = false
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return entity.Id;
        }
    }
}
