using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserLogin.Queries.GetAllUserLogins
{
    public record GetAllUserLoginsQuery() : IRequest<List<ResultUserLoginDTO>>;
}


