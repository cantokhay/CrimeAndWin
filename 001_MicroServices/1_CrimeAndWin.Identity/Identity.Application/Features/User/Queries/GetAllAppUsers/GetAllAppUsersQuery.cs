using Identity.Application.DTOs.UserDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.User.Queries.GetAllAppUsers
{
    public record GetAllAppUsersQuery() : IRequest<List<ResultAppUserDTO>>;
}
