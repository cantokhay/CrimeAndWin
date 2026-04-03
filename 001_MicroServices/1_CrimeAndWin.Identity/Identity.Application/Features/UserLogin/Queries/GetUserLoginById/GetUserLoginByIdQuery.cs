using Identity.Application.DTOs.UserLoginDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.UserLogin.Queries.GetUserLoginById
{
    public record GetUserLoginByIdQuery(Guid id) : IRequest<ResultUserLoginDTO>;
}


