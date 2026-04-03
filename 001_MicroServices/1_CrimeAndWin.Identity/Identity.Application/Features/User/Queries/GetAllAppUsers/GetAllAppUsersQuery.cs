using Identity.Application.DTOs.UserDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Queries.GetAllAppUsers
{
    public record GetAllAppUsersQuery() : IRequest<List<ResultAppUserDTO>>;
}


