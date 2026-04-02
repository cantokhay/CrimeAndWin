using Identity.Application.DTOs.UserDTOs.Admin;
using Mediator;

namespace Identity.Application.Features.User.Queries.GetAppUserForAdminById
{
    public sealed record GetAppUserForAdminByIdQuery(Guid id) : IRequest<ResultAppUserDTO?>;
}

