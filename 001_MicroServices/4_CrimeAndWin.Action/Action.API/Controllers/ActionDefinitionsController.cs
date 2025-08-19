using Action.Application.DTOs;
using Action.Application.Features.ActionDefinitons.Commands;
using Action.Application.Features.ActionDefinitons.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    }
}
