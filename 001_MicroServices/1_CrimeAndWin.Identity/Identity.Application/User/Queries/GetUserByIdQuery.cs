using Identity.Application.User.DTOs;
using MediatR;

namespace Identity.Application.User.Queries
{
    public sealed class GetUserByIdQuery : IRequest<AppUserDTO?>
    {
        public Guid Id { get; set; }
    }
}
