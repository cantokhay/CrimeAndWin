using AutoMapper;
using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using Identity.Application.DTOs.RoleDTOs.Admin;
using Identity.Application.DTOs.UserClaimDTOs.Admin;
using Identity.Application.DTOs.UserDTOs.Admin;
using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Identity.Application.DTOs.UserRoleDTOs.Admin;
using Identity.Application.DTOs.UserTokenDTOs.Admin;
using Identity.Domain.Entities;

namespace Identity.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // AppUser
            CreateMap<AppUser, ResultAppUserDTO>().ReverseMap();
            CreateMap<AppUser, CreateAppUserDTO>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserDTO>().ReverseMap();

            // Role
            CreateMap<Role, ResultRoleDTO>().ReverseMap();
            CreateMap<Role, CreateRoleDTO>().ReverseMap();
            CreateMap<Role, UpdateRoleDTO>().ReverseMap();

            // UserRole
            CreateMap<UserRole, ResultUserRoleDTO>().ReverseMap();
            CreateMap<UserRole, CreateUserRoleDTO>().ReverseMap();
            CreateMap<UserRole, UpdateUserRoleDTO>().ReverseMap();

            // UserClaim
            CreateMap<UserClaim, ResultUserClaimDTO>().ReverseMap();
            CreateMap<UserClaim, CreateUserClaimDTO>().ReverseMap();
            CreateMap<UserClaim, UpdateUserClaimDTO>().ReverseMap();

            // UserLogin
            CreateMap<UserLogin, ResultUserLoginDTO>().ReverseMap();
            CreateMap<UserLogin, CreateUserLoginDTO>().ReverseMap();
            CreateMap<UserLogin, UpdateUserLoginDTO>().ReverseMap();

            // UserToken
            CreateMap<UserToken, ResultUserTokenDTO>().ReverseMap();
            CreateMap<UserToken, CreateUserTokenDTO>().ReverseMap();
            CreateMap<UserToken, UpdateUserTokenDTO>().ReverseMap();

            // RefreshToken
            CreateMap<RefreshToken, ResultRefreshTokenDTO>().ReverseMap();
            CreateMap<RefreshToken, CreateRefreshTokenDTO>().ReverseMap();
            CreateMap<RefreshToken, UpdateRefreshTokenDTO>().ReverseMap();
        }
    }
}
