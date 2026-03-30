using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Application.DTOs.Admin;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Queries.GetAllNotificationsAsAdmin
{
    public sealed class GetAllNotificationsAsAdminQueryHandler
            : IRequestHandler<GetAllNotificationsAsAdminQuery, List<AdminResultNotificationDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Notification> _read;

        public GetAllNotificationsAsAdminQueryHandler(IReadRepository<Domain.Entities.Notification> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultNotificationDTO>> Handle(GetAllNotificationsAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(n => new AdminResultNotificationDTO
                {
                    Id = n.Id,
                    PlayerId = n.PlayerId,
                    Title = n.Content.Title,
                    Message = n.Content.Message,
                    Type = n.Content.Type,
                    CreatedAtUtc = n.CreatedAtUtc,
                    UpdatedAtUtc = n.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
