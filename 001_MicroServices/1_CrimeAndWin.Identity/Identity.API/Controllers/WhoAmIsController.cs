using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class WhoAmIsController : ControllerBase
    {
        [HttpGet]
        [Authorize] // giriş yapmış herkes
        public IActionResult Get()
        => Ok(new
        {
            UserId = User.FindFirst("sub")?.Value,
            UserName = User.Identity?.Name,        // unique_name
            Roles = User.Claims.Where(c => c.Type.EndsWith("/role") || c.Type == "role").Select(c => c.Value)
        });

        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")] // yalnız Admin
        public IActionResult AdminPing() => Ok("admin ok");

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            return Ok(new
            {
                Name = User.Identity?.Name,            // unique_name
                Sub = User.FindFirstValue(JwtRegisteredClaimNames.Sub),
                Email = User.FindFirstValue(JwtRegisteredClaimNames.Email),
                Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
            });
        }
    }
}
