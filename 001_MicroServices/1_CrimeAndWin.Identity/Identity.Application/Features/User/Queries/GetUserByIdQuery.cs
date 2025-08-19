using Identity.Application.DTOs.UserDTOs;
using MediatR;

namespace Identity.Application.Features.User.Queries
{
    public sealed class GetUserByIdQuery : IRequest<AppUserDTO?>
    {
        public Guid Id { get; set; }
    }
}
