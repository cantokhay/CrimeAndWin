using Identity.Application.DTOs.UserDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Queries.GetUserById
{
    public sealed class GetUserByIdQuery : IRequest<AppUserDTO?>
    {
        public Guid id { get; set; }
    }
}


