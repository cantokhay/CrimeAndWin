using Identity.Application.Features.User.Commands.RegisterUser;
using Identity.Application.Features.User.Commands.ConfirmEmail;
using Shared.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegistersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken ct)
        {
            try
            {
                var created = await _mediator.Send(command, ct);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return ValidationProblem(title: "Geçersiz alan", detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (InvalidOperationException ex) 
            {
                return Problem(title: "Çakışma", detail: ex.Message, statusCode: StatusCodes.Status409Conflict);
            }
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id) => Ok(new { id });

        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token, CancellationToken ct)
        {
            var command = new ConfirmEmailCommand(email, token);
            var success = await _mediator.Send(command, ct);

            if (success)
            {
                return Ok(new { message = "E-posta başarıyla onaylandı. Hesabınız Admin onayı bekliyor." });
            }

            return BadRequest(new { message = "E-posta onaylanamadı. Geçersiz token veya kullanıcı." });
        }
    }
}
