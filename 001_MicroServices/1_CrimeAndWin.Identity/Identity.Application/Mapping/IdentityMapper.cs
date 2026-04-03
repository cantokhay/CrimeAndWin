using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Identity.Application.DTOs.RoleDTOs.Admin;
using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Identity.Application.DTOs.UserDTOs.Admin;
using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Identity.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Identity.Application.Mapping;

[Mapper]
public partial class IdentityMapper
{
    // AppUser
    public partial ResultAppUserDTO ToResultDto(AppUser entity);
    public partial AppUser ToEntity(ResultAppUserDTO dto);
    public partial AppUser ToEntity(CreateAppUserDTO dto);
    public partial AppUser ToEntity(UpdateAppUserDTO dto);

    // Role
    public partial ResultRoleDTO ToResultDto(Role entity);
    public partial Role ToEntity(ResultRoleDTO dto);
    public partial Role ToEntity(CreateRoleDTO dto);
    public partial Role ToEntity(UpdateRoleDTO dto);

    // UserRole
    public partial ResultUserRoleDTO ToResultDto(UserRole entity);
    public partial UserRole ToEntity(ResultUserRoleDTO dto);
    public partial UserRole ToEntity(CreateUserRoleDTO dto);
    public partial UserRole ToEntity(UpdateUserRoleDTO dto);

    // UserClaim
    public partial ResultUserClaimDTO ToResultDto(UserClaim entity);
    public partial UserClaim ToEntity(ResultUserClaimDTO dto);
    public partial UserClaim ToEntity(CreateUserClaimDTO dto);
    public partial UserClaim ToEntity(UpdateUserClaimDTO dto);

    // UserLogin
    public partial ResultUserLoginDTO ToResultDto(UserLogin entity);
    public partial UserLogin ToEntity(ResultUserLoginDTO dto);
    public partial UserLogin ToEntity(CreateUserLoginDTO dto);
    public partial UserLogin ToEntity(UpdateUserLoginDTO dto);

    // UserToken
    public partial ResultUserTokenDTO ToResultDto(UserToken entity);
    public partial UserToken ToEntity(ResultUserTokenDTO dto);
    public partial UserToken ToEntity(CreateUserTokenDTO dto);
    public partial UserToken ToEntity(UpdateUserTokenDTO dto);

    // RefreshToken
    public partial ResultRefreshTokenDTO ToResultDto(RefreshToken entity);
    public partial RefreshToken ToEntity(ResultRefreshTokenDTO dto);
    public partial RefreshToken ToEntity(CreateRefreshTokenDTO dto);
    public partial RefreshToken ToEntity(UpdateRefreshTokenDTO dto);

    // Collection Mappings
    public partial IEnumerable<ResultAppUserDTO> ToResultDtoList(IEnumerable<AppUser> entities);
    public partial IEnumerable<ResultRoleDTO> ToResultDtoList(IEnumerable<Role> entities);
}


