using MediatR;
using Notification.Application.DTOs.Admin;
using Shared.Domain.Repository;

namespace Notification.Application.Features.Notification.Queries.GetNotificationByIdAsAdmin
{
    public sealed class GetNotificationByIdAsAdminQueryHandler
            : IRequestHandler<GetNotificationByIdAsAdminQuery, AdminResultNotificationDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Notification> _read;

        public GetNotificationByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Notification> read)
        {
            _read = read;
        }

        public async Task<AdminResultNotificationDTO?> Handle(GetNotificationByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var n = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (n is null) return null;

            return new AdminResultNotificationDTO
            {
                Id = n.Id,
                PlayerId = n.PlayerId,
                Title = n.Content.Title,
                Message = n.Content.Message,
                Type = n.Content.Type,
                CreatedAtUtc = n.CreatedAtUtc,
                UpdatedAtUtc = n.UpdatedAtUtc
            };
        }
    }
}
