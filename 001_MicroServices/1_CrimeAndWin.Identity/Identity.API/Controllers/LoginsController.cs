using Identity.Application.DTOs.AuthDTOs;
using Identity.Application.Features.Auth.Commands.Login;
using Mediator;
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

        /// <summary>Kullanýcý giriþi yapar ve JWT üretir.</summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResultAuthDTO), StatusCodes.Status200OK)]
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

