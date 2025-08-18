using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class _DebugsController : ControllerBase
    {
        [HttpGet("auth-header")]
        [AllowAnonymous]
        public IActionResult AuthHeader()
        => Ok(new { Authorization = Request.Headers["Authorization"].ToString() });

        [HttpGet("token-info")]
        [AllowAnonymous]
        public IActionResult TokenInfo([FromHeader(Name = "Authorization")] string? auth)
        {
            if (string.IsNullOrWhiteSpace(auth)) return BadRequest("Authorization header yok.");
            var token = auth.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries).Last();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            return Ok(new
            {
                jwt.Issuer,
                Audiences = jwt.Audiences.ToArray(),
                ValidFromUtc = jwt.ValidFrom,
                ValidToUtc = jwt.ValidTo,
                Claims = jwt.Claims.Select(c => new { c.Type, c.Value }).ToArray()
            });
        }
    }
}
