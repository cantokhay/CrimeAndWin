using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moderation.Application.DTOs.ReportDTOs;
using Shared.Domain.Repository;

namespace Moderation.Application.Features.Report.Queries
{
    public class GetReportsHandlers :
            IRequestHandler<GetReportsByPlayerIdQuery, List<ResultReportDTO>>,
            IRequestHandler<GetOpenReportsQuery, List<ResultReportDTO>>
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

        public async Task<List<ResultReportDTO>> Handle(GetOpenReportsQuery request, CancellationToken ct)
        {
            var query = _readRepo.GetWhere(x => !x.IsResolved, tracking: false);
            var data = await query.OrderBy(x => x.CreatedAtUtc).ToListAsync(ct);
            return _mapper.Map<List<ResultReportDTO>>(data);
        }
    }
}
