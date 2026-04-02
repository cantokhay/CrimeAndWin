using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.UserLogin.Queries.GetUserLoginById
{
    public record GetUserLoginByIdQuery(Guid id) : IRequest<ResultUserLoginDTO>;
}

