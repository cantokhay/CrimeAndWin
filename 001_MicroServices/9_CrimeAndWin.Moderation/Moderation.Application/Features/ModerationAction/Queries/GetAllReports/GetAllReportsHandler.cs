using Moderation.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ReportDTOs;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.ModerationAction.Queries.GetAllReports
{
    public sealed class GetAllReportsHandler(
            IReadRepository<Domain.Entities.Report> readRepo,
            ModerationMapper mapper)
            : IRequestHandler<GetAllReportsQuery, List<ResultReportDTO>>
    {
        public async Task<List<ResultReportDTO>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            var reports = await readRepo.Table
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync(cancellationToken);

            return mapper.ToResultDtoList(reports).ToList();
        }
    }
}



