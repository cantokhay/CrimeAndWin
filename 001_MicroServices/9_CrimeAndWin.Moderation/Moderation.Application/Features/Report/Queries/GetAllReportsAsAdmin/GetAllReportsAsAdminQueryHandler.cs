using MediatR;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ReportDTOs.Admin;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Queries.GetAllReportsAsAdmin
{
    public sealed class GetAllReportsAsAdminQueryHandler
        : IRequestHandler<GetAllReportsAsAdminQuery, List<AdminResultReportDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Report> _read;

        public GetAllReportsAsAdminQueryHandler(IReadRepository<Domain.Entities.Report> read)
        {
            _read = read;
        }

        public async Task<List<AdminResultReportDTO>> Handle(GetAllReportsAsAdminQuery request, CancellationToken cancellationToken)
        {
            return await _read.GetAll(false)
                .Select(r => new AdminResultReportDTO
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
                })
                .ToListAsync(cancellationToken);
        }
    }
}
