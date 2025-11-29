using Action.Application.DTOs.ActionDefinitionDTOs;
using Action.Application.Features.ActionDefinitons.Commands.CreateAction;
using Action.Application.Features.ActionDefinitons.Commands.Seed;
using Action.Application.Features.ActionDefinitons.Queries.GetAllAction;
using Action.Application.Features.ActionDefinitons.Queries.GetByCodeAction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Action.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionDefinitionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ActionDefinitionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActionDefinitionDTO request)
        {
            var result = await _mediator.Send(new CreateActionDefinitionCommand(request));
            return CreatedAtAction(nameof(GetByCode), new { code = result.Code }, result);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var result = await _mediator.Send(new GetByCodeActionDefinitionQuery(code));
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Tüm ActionDefinition kayıtlarını listeler.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ResultActionDefinitionDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllActionDefinitionsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Bogus ile rastgele ActionDefinition verileri oluşturur.
        /// </summary>
        [HttpPost("SeedRun")]
        public async Task<IActionResult> SeedRun([FromQuery] int count = 10)
        {
            await _mediator.Send(new RunActionSeedCommand(count));
            return Ok(new { message = $"{count} adet ActionDefinition başarıyla seed edildi." });
        }
    }
}
