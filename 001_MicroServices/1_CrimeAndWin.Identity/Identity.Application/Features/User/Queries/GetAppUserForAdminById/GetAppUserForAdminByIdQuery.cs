using Identity.Application.DTOs.UserDTOs.Admin;
using MediatR;

namespace Identity.Application.Features.User.Queries.GetAppUserForAdminById
{
    public sealed record GetAppUserForAdminByIdQuery(Guid id) : IRequest<ResultAppUserDTO?>;
}
