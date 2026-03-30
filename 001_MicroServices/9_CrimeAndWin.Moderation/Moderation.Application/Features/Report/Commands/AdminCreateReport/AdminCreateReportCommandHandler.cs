using MediatR;
using Moderation.Domain.VOs;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Moderation.Application.Features.Report.Commands.AdminCreateReport
{
    public sealed class AdminCreateReportCommandHandler
            : IRequestHandler<AdminCreateReportCommand, Guid>
    {
        private readonly IWriteRepository<Domain.Entities.Report> _write;
        private readonly IDateTimeProvider _time;

        public AdminCreateReportCommandHandler(IWriteRepository<Domain.Entities.Report> write, IDateTimeProvider time)
        {
            _write = write;
            _time = time;
        }

        public async Task<Guid> Handle(AdminCreateReportCommand request, CancellationToken cancellationToken)
        {
            var d = request.createReportDTO;

            var entity = new Domain.Entities.Report
            {
                Id = Guid.NewGuid(),
                ReporterId = d.ReporterId,
                ReportedPlayerId = d.ReportedPlayerId,
                Reason = new ReportReason(d.Reason),
                Description = d.Description,
                IsResolved = d.IsResolved,
                ResolvedAtUtc = d.ResolvedAtUtc,
                ResolvedByModeratorId = d.ResolvedByModeratorId,
                CreatedAtUtc = _time.UtcNow,
                IsDeleted = false
            };

            await _write.AddAsync(entity);
            await _write.SaveAsync();

            return entity.Id;
        }
    }
}
