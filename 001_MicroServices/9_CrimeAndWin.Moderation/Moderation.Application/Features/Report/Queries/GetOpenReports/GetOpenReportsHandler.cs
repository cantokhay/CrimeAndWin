using Moderation.Application.Mapping;
using Shared.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ReportDTOs;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Queries.GetOpenReports
{
    public class GetOpenReportsHandler : IRequestHandler<GetOpenReportsQuery, List<ResultReportDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Report> _readRepo;
        private readonly ModerationMapper _mapper;

        public GetOpenReportsHandler(IReadRepository<Domain.Entities.Report> readRepo, ModerationMapper mapper)
        {
            _readRepo = readRepo;
            _mapper = mapper;
        }
        
        public async Task<List<ResultReportDTO>> Handle(GetOpenReportsQuery request, CancellationToken ct)
        {
            var query = _readRepo.GetWhere(x => !x.IsResolved, tracking: false);
            var data = await query.OrderBy(x => x.CreatedAtUtc).ToListAsync(ct);
            return _mapper.ToResultDtoList(data).ToList();
        }
    }
}



