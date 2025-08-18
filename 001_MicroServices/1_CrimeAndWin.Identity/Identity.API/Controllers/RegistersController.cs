using Identity.Application.Features.User.Commands.RegisterUser;
using Identity.Application.Features.User.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegistersController(IMediator mediator) => _mediator = mediator;

        /// <summary>Kullanıcı kaydı oluşturur.</summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AppUserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken ct)
        {
            try
            {
                var created = await _mediator.Send(command, ct);
                // Location header isteğe bağlı: kaynağı temsil eden basit bir URL
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return ValidationProblem(title: "Geçersiz alan", detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            catch (InvalidOperationException ex) // ör. e-posta/username benzersiz değil
            {
                return Problem(title: "Çakışma", detail: ex.Message, statusCode: StatusCodes.Status409Conflict);
            }
        }

        /// <summary>Basit demo amaçlı: oluşturulan kullanıcıyı görmek için (isteğe bağlı)</summary>
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id) => Ok(new { id }); // gerçek okuma için query/handler eklenebilir
    }
}
