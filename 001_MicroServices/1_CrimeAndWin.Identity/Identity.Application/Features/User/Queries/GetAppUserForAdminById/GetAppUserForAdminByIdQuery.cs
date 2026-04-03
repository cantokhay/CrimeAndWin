using Identity.Application.DTOs.UserDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Queries.GetAppUserForAdminById
{
    public sealed record GetAppUserForAdminByIdQuery(Guid id) : IRequest<ResultAppUserDTO?>;
}


