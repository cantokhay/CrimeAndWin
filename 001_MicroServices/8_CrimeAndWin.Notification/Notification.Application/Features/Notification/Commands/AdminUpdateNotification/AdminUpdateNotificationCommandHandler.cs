using MediatR;
using Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Notification.Domain.VOs;

namespace Notification.Application.Features.Notification.Commands.AdminUpdateNotification
{
    public sealed class AdminUpdateNotificationCommandHandler
            : IRequestHandler<AdminUpdateNotificationCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Notification> _read;
        private readonly IWriteRepository<Domain.Entities.Notification> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateNotificationCommandHandler(
            IReadRepository<Domain.Entities.Notification> read,
            IWriteRepository<Domain.Entities.Notification> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            var d = request.updateNotificationDTO;

            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.PlayerId = d.PlayerId;
            entity.Content = new NotificationContent(d.Title, d.Message, d.Type);

            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();

            return ok;
        }
    }
}
