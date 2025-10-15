using Identity.Application.DTOs.UserDTOs.Admin;
using Identity.Application.Features.User.Queries.GetUserById;
using Identity.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Queries.GetAppUserForAdminById
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetAppUserForAdminByIdQuery, ResultAppUserDTO?>
    {
        private readonly IReadRepository<AppUser> _read;
        public GetUserByIdQueryHandler(IReadRepository<AppUser> read) => _read = read;

        public async Task<ResultAppUserDTO?> Handle(GetAppUserForAdminByIdQuery request, CancellationToken ct)
        {
            var user = await _read.GetByIdAsync(request.id.ToString(), tracking: false);
            if (user is null) return null;

            return new ResultAppUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                AccessFailedCount = user.AccessFailedCount,
                CreatedAtUtc = user.CreatedAtUtc,
                UpdatedAtUtc = user.UpdatedAtUtc
            };
        }
    }
}
