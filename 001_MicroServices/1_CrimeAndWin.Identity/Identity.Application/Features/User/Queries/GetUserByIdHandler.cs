using Identity.Application.Features.User.DTOs;
using Identity.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Queries
{
    public sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, AppUserDTO?>
    {
        private readonly IReadRepository<AppUser> _read;
        public GetUserByIdHandler(IReadRepository<AppUser> read) => _read = read;

        public async Task<AppUserDTO?> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await _read.GetByIdAsync(request.Id.ToString(), tracking: false);
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
