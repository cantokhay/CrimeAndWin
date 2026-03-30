using MediatR;
using Moderation.Application.DTOs.ReportDTOs.Admin;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Queries.GetReportByIdAsAdmin
{
    public sealed class GetReportByIdAsAdminQueryHandler
           : IRequestHandler<GetReportByIdAsAdminQuery, AdminResultReportDTO?>
    {
        private readonly IReadRepository<Domain.Entities.Report> _read;

        public GetReportByIdAsAdminQueryHandler(IReadRepository<Domain.Entities.Report> read)
        {
            _read = read;
        }

        public async Task<AdminResultReportDTO?> Handle(GetReportByIdAsAdminQuery request, CancellationToken cancellationToken)
        {
            var r = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (r is null) return null;

            return new AdminResultReportDTO
            {
                Id = r.Id,
                ReporterId = r.ReporterId,
                ReportedPlayerId = r.ReportedPlayerId,
                Reason = r.Reason.Value,
                Description = r.Description,
                IsResolved = r.IsResolved,
                ResolvedAtUtc = r.ResolvedAtUtc,
                ResolvedByModeratorId = r.ResolvedByModeratorId,
                CreatedAtUtc = r.CreatedAtUtc,
                UpdatedAtUtc = r.UpdatedAtUtc
            };
        }
    }
}
