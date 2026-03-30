using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moderation.Application.DTOs.ReportDTOs;
using Moderation.Application.Features.ModerationAction.Queries.GetAllReports;
using Moderation.Application.Features.Report.Commands.CreateReport;
using Moderation.Application.Features.Report.Commands.ResolveReport;
using Moderation.Application.Features.Report.Commands.Seed;
using Moderation.Application.Features.Report.Queries.GetOpenReports;
using Moderation.Application.Features.Report.Queries.GetRportsByPlayerId;

namespace Moderation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReportsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        // [Authorize(Roles = "Player,Moderator,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateReportDTO dto)
        {
            var id = await _mediator.Send(new CreateReportCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpPut("{id:guid}/resolve")]
        // [Authorize(Roles = "Moderator,Admin")]
        public async Task<IActionResult> Resolve(Guid id, [FromQuery] Guid moderatorId)
        {
            var ok = await _mediator.Send(new ResolveReportCommand(id, moderatorId));
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var list = await _mediator.Send(new GetReportsByPlayerIdQuery(Guid.Empty)); // demo amaçlı
            var item = list.FirstOrDefault(x => x.Id == id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? reportedPlayerId, [FromQuery] bool? isResolved)
        {
            if (reportedPlayerId.HasValue)
                return Ok(await _mediator.Send(new GetReportsByPlayerIdQuery(reportedPlayerId.Value)));

            if (isResolved.HasValue && !isResolved.Value)
                return Ok(await _mediator.Send(new GetOpenReportsQuery()));

            return Ok(await _mediator.Send(new GetOpenReportsQuery())); // basit varsayılan
        }

        /// <summary>
        /// Tüm raporları listeler.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultReportDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllReportsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Rastgele Report ve ModerationAction verilerini oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunModerationSeedCommand(count));
            return Ok(new { message = $"{count} adet Report ve ModerationAction başarıyla seed edildi." });
        }
    }
}
