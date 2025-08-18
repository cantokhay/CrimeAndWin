using Identity.Application.Features.User.DTOs;
using MediatR;

namespace Identity.Application.Features.User.Queries
{
    public sealed class GetUserByIdQuery : IRequest<AppUserDTO?>
    {
        public Guid Id { get; set; }
    }
}
