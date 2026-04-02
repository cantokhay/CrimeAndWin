using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserLogin.Queries.GetAllUserLogins
{
    public record GetAllUserLoginsQuery() : IRequest<List<ResultUserLoginDTO>>;
}

