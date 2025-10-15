using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Identity.Application.DTOs.RoleDTOs.Admin;
using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Identity.Application.DTOs.UserDTOs.Admin;
using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Identity.Application.Features.RefreshToken.Commands.CreateRefreshToken;
using Identity.Application.Features.RefreshToken.Commands.DeleteRefreshToken;
using Identity.Application.Features.RefreshToken.Commands.UpdateRefreshToken;
using Identity.Application.Features.RefreshToken.Queries.GetAllRefreshTokens;
using Identity.Application.Features.RefreshToken.Queries.GetRefreshTokenById;
using Identity.Application.Features.Role.Commands.CreateRole;
using Identity.Application.Features.Role.Commands.DeleteRole;
using Identity.Application.Features.Role.Commands.UpdateRole;
using Identity.Application.Features.Role.Queries.GetAllRoles;
using Identity.Application.Features.Role.Queries.GetRoleById;
using Identity.Application.Features.User.Commands.CreateAppUser;
using Identity.Application.Features.User.Commands.DeleteAppUser;
using Identity.Application.Features.User.Commands.UpdateAppUser;
using Identity.Application.Features.User.Queries.GetAllAppUsers;
using Identity.Application.Features.User.Queries.GetAppUserForAdminById;
using Identity.Application.Features.UserClaim.Commands.CreateUserClaim;
using Identity.Application.Features.UserClaim.Commands.DeleteUserClaim;
using Identity.Application.Features.UserClaim.Commands.UpdateUserClaim;
using Identity.Application.Features.UserClaim.Queries.GetAllUserClaims;
using Identity.Application.Features.UserClaim.Queries.GetUserClaimById;
using Identity.Application.Features.UserLogin.Commands.CreateUserLogin;
using Identity.Application.Features.UserLogin.Commands.DeleteUserLogin;
using Identity.Application.Features.UserLogin.Commands.UpdateUserLogin;
using Identity.Application.Features.UserLogin.Queries.GetAllUserLogins;
using Identity.Application.Features.UserLogin.Queries.GetUserLoginById;
using Identity.Application.Features.UserRole.Commands.CreateUserRole;
using Identity.Application.Features.UserRole.Commands.DeleteUserRole;
using Identity.Application.Features.UserRole.Commands.UpdateUserRole;
using Identity.Application.Features.UserRole.Queries.GetAllUserRoles;
using Identity.Application.Features.UserRole.Queries.GetUserRoleById;
using Identity.Application.Features.UserToken.Commands.CreateUserToken;
using Identity.Application.Features.UserToken.Commands.DeleteUserToken;
using Identity.Application.Features.UserToken.Commands.UpdateUserToken;
using Identity.Application.Features.UserToken.Queries.GetAllUserTokens;
using Identity.Application.Features.UserToken.Queries.GetUserTokenById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityAdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IdentityAdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // -------------------------------
        // APP USER CRUD
        // -------------------------------
        [HttpGet("GetAllAppUsers")]
        public async Task<IActionResult> GetAllAppUsers()
        {
            var result = await _mediator.Send(new GetAllAppUsersQuery());
            return Ok(result);
        }

        [HttpGet("GetByIdAppUser/{id:guid}")]
        public async Task<IActionResult> GetByIdAppUser(Guid id)
        {
            var item = await _mediator.Send(new GetAppUserForAdminByIdQuery(id));
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("CreateAppUser")]
        public async Task<IActionResult> CreateAppUser([FromBody] CreateAppUserDTO dto)
        {
            var id = await _mediator.Send(new CreateAppUserCommand(dto));
            return Ok(id);
        }

        [HttpPut("UpdateAppUser")]
        public async Task<IActionResult> UpdateAppUser([FromBody] UpdateAppUserDTO dto)
        {
            var result = await _mediator.Send(new UpdateAppUserCommand(dto));
            return Ok(result);
        }

        [HttpDelete("DeleteAppUser/{id:guid}")]
        public async Task<IActionResult> DeleteAppUser(Guid id)
        {
            var result = await _mediator.Send(new DeleteAppUserCommand(id));
            return result ? Ok() : NotFound();
        }

        // -------------------------------
        // ROLE CRUD
        // -------------------------------
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRolesQuery());
            return Ok(result);
        }

        [HttpGet("GetByIdRole/{id:guid}")]
        public async Task<IActionResult> GetByIdRole(Guid id)
        {
            var item = await _mediator.Send(new GetRoleByIdQuery(id));
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO dto)
        {
            var id = await _mediator.Send(new CreateRoleCommand(dto));
            return Ok(id);
        }

        [HttpDelete("DeleteRole/{id:guid}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleDTO dto)
        {
            var result = await _mediator.Send(new UpdateRoleCommand(dto));
            return Ok(result);
        }

        // -------------------------------
        // USER ROLE CRUD
        // -------------------------------
        [HttpGet("GetAllUserRoles")]
        public async Task<IActionResult> GetAllUserRoles()
        {
            var result = await _mediator.Send(new GetAllUserRolesQuery());
            return Ok(result);
        }

        [HttpGet("GetByIdUserRole/{id:guid}")]
        public async Task<IActionResult> GetByIdUserRole(Guid id)
        {
            var item = await _mediator.Send(new GetUserRoleByIdQuery(id));
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("CreateUserRole")]
        public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleDTO dto)
        {
            var id = await _mediator.Send(new CreateUserRoleCommand(dto));
            return Ok(id);
        }

        [HttpDelete("DeleteUserRole/{id:guid}")]
        public async Task<IActionResult> DeleteUserRole(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserRoleCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDTO dto)
        {
            var result = await _mediator.Send(new UpdateUserRoleCommand(dto));
            return Ok(result);
        }

        // -------------------------------
        // USER CLAIM CRUD
        // -------------------------------
        [HttpGet("GetAllUserClaims")]
        public async Task<IActionResult> GetAllUserClaims()
        {
            var result = await _mediator.Send(new GetAllUserClaimsQuery());
            return Ok(result);
        }

        [HttpGet("GetByIdUserClaim/{id:guid}")]
        public async Task<IActionResult> GetByIdUserClaim(Guid id)
        {
            var item = await _mediator.Send(new GetUserClaimByIdQuery(id));
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("CreateUserClaim")]
        public async Task<IActionResult> CreateUserClaim([FromBody] CreateUserClaimDTO dto)
        {
            var id = await _mediator.Send(new CreateUserClaimCommand(dto));
            return Ok(id);
        }

        [HttpDelete("DeleteUserClaim/{id:guid}")]
        public async Task<IActionResult> DeleteUserClaim(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserClaimCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpPut("UpdateUserClaim")]
        public async Task<IActionResult> UpdateUserClaim([FromBody] UpdateUserClaimDTO dto)
        {
            var result = await _mediator.Send(new UpdateUserClaimCommand(dto));
            return Ok(result);
        }

        // -------------------------------
        // USER LOGIN CRUD
        // -------------------------------
        [HttpGet("GetAllUserLogins")]
        public async Task<IActionResult> GetAllUserLogins()
        {
            var result = await _mediator.Send(new GetAllUserLoginsQuery());
            return Ok(result);
        }

        [HttpGet("GetByIdUserLogin/{id:guid}")]
        public async Task<IActionResult> GetByIdUserLogins(Guid id)
        {
            var item = await _mediator.Send(new GetUserLoginByIdQuery(id));
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("CreateUserLogin")]
        public async Task<IActionResult> CreateUserLogin([FromBody] CreateUserLoginDTO dto)
        {
            var id = await _mediator.Send(new CreateUserLoginCommand(dto));
            return Ok(id);
        }

        [HttpDelete("DeleteUserLogin/{id:guid}")]
        public async Task<IActionResult> DeleteUserLogin(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserLoginCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpPut("UpdateUserLogin")]
        public async Task<IActionResult> UpdateUserLogin([FromBody] UpdateUserLoginDTO dto)
        {
            var result = await _mediator.Send(new UpdateUserLoginCommand(dto));
            return Ok(result);
        }

        // -------------------------------
        // USER TOKEN CRUD
        // -------------------------------
        [HttpGet("GetAllUserTokens")]
        public async Task<IActionResult> GetAllUserTokens()
        {
            var result = await _mediator.Send(new GetAllUserTokensQuery());
            return Ok(result);
        }

        [HttpGet("GetUserTokenById/{id:guid}")]
        public async Task<IActionResult> GetUserTokenById(Guid id)
        {
            var item = await _mediator.Send(new GetUserTokenByIdQuery(id));
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("CreateUserToken")]
        public async Task<IActionResult> CreateUserToken([FromBody] CreateUserTokenDTO dto)
        {
            var id = await _mediator.Send(new CreateUserTokenCommand(dto));
            return Ok(id);
        }

        [HttpDelete("DeleteUserToken/{id:guid}")]
        public async Task<IActionResult> DeleteUserToken(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserTokenCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpPut("UpdateUserToken")]
        public async Task<IActionResult> UpdateUserToken([FromBody] UpdateUserTokenDTO dto)
        {
            var result = await _mediator.Send(new UpdateUserTokenCommand(dto));
            return Ok(result);
        }

        // -------------------------------
        // REFRESH TOKEN CRUD
        // -------------------------------
        [HttpGet("GetAllRefreshTokens")]
        public async Task<IActionResult> GetAllRefreshTokens()
        {
            var result = await _mediator.Send(new GetAllRefreshTokensQuery());
            return Ok(result);
        }

        [HttpGet("GetRefreshTokenById/{id:guid}")]
        public async Task<IActionResult> GetRefreshTokenById(Guid id)
        {
            var item = await _mediator.Send(new GetRefreshTokenByIdQuery(id));
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost("CreateRefreshToken")]
        public async Task<IActionResult> CreateRefreshToken([FromBody] CreateRefreshTokenDTO dto)
        {
            var id = await _mediator.Send(new CreateRefreshTokenCommand(dto));
            return Ok(id);
        }

        [HttpDelete("DeleteRefreshToken/{id:guid}")]
        public async Task<IActionResult> DeleteRefreshToken(Guid id)
        {
            var result = await _mediator.Send(new DeleteRefreshTokenCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpPut("UpdateRefreshToken")]
        public async Task<IActionResult> UpdateRefreshToken([FromBody] UpdateRefreshTokenDTO dto)
        {
            var result = await _mediator.Send(new UpdateRefreshTokenCommand(dto));
            return Ok(result);
        }
    }
}
