using MediatR;
using Moderation.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Moderation.Application.Features.Report.Commands.AdminUpdateReport
{
    public sealed class AdminUpdateReportCommandHandler
            : IRequestHandler<AdminUpdateReportCommand, bool>
    {
        private readonly IReadRepository<Domain.Entities.Report> _read;
        private readonly IWriteRepository<Domain.Entities.Report> _write;
        private readonly IDateTimeProvider _time;

        public AdminUpdateReportCommandHandler(
            IReadRepository<Domain.Entities.Report> read,
            IWriteRepository<Domain.Entities.Report> write,
            IDateTimeProvider time)
        {
            _read = read;
            _write = write;
            _time = time;
        }

        public async Task<bool> Handle(AdminUpdateReportCommand request, CancellationToken cancellationToken)
        {
            var d = request.updateReportDTO;

            var entity = await _read.GetByIdAsync(d.Id.ToString(), tracking: true);
            if (entity is null) return false;

            entity.ReporterId = d.ReporterId;
            entity.ReportedPlayerId = d.ReportedPlayerId;
            entity.Reason = new ReportReason(d.Reason);
            entity.Description = d.Description;
            entity.IsResolved = d.IsResolved;
            entity.ResolvedAtUtc = d.ResolvedAtUtc;
            entity.ResolvedByModeratorId = d.ResolvedByModeratorId;
            entity.UpdatedAtUtc = _time.UtcNow;

            var ok = _write.Update(entity);
            await _write.SaveAsync();

            return ok;
        }
    }
}
