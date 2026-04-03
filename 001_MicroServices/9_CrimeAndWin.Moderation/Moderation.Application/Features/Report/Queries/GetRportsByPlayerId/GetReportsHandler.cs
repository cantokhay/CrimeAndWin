using Moderation.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ReportDTOs;
using Moderation.Application.Features.Report.Queries.GetOpenReports;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Queries.GetRportsByPlayerId
{
    public class GetReportsHandlers :
            IRequestHandler<GetReportsByPlayerIdQuery, List<ResultReportDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Report> _readRepo;
        private readonly ModerationMapper _mapper;

        public GetReportsHandlers(IReadRepository<Domain.Entities.Report> readRepo, ModerationMapper mapper)
        {
            _readRepo = readRepo;
            _mapper = mapper;
        }

        public async Task<List<ResultReportDTO>> Handle(GetReportsByPlayerIdQuery request, CancellationToken ct)
        {
            var query = _readRepo.GetWhere(x => x.ReportedPlayerId == request.ReportedPlayerId, tracking: false);
            var data = await query.OrderByDescending(x => x.CreatedAtUtc).ToListAsync(ct);
            return _mapper.ToResultDtoList(data).ToList();
        }
    }
}



