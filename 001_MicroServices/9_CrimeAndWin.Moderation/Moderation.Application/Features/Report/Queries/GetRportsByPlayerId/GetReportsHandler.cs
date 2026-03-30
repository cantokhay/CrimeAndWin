using AutoMapper;
using MediatR;
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
        private readonly IMapper _mapper;

        public GetReportsHandlers(IReadRepository<Domain.Entities.Report> readRepo, IMapper mapper)
        {
            _readRepo = readRepo;
            _mapper = mapper;
        }

        public async Task<List<ResultReportDTO>> Handle(GetReportsByPlayerIdQuery request, CancellationToken ct)
        {
            var query = _readRepo.GetWhere(x => x.ReportedPlayerId == request.ReportedPlayerId, tracking: false);
            var data = await query.OrderByDescending(x => x.CreatedAtUtc).ToListAsync(ct);
            return _mapper.Map<List<ResultReportDTO>>(data);
        }
    }
}
