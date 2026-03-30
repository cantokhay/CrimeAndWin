using Notification.Domain.VOs;
using Shared.Domain;

namespace Notification.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public NotificationContent Content { get; set; }
    }
}
