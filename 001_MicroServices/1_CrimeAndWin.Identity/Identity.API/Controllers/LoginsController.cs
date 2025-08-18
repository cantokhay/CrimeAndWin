using Identity.Application.Auth.Commands.Login;
using Identity.Application.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Kullanıcı girişi yapar ve JWT üretir.</summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResultDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
        {
            try
            {
                var result = await _mediator.Send(command, ct);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Problem(title: "Yetkisiz", detail: ex.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
            catch (ArgumentException ex)
            {
                return ValidationProblem(title: "Geçersiz alan", detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }
    }
}
