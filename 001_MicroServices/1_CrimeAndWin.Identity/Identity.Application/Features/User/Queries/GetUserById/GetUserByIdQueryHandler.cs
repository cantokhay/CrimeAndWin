using Identity.Application.DTOs.UserDTOs;
using Identity.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Queries.GetUserById
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AppUserDTO?>
    {
        private readonly IReadRepository<AppUser> _read;
        public GetUserByIdQueryHandler(IReadRepository<AppUser> read) => _read = read;

        public async Task<AppUserDTO?> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (user is null) return null;

            return new AppUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
        }
    }
}
