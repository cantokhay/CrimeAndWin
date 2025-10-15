using Identity.Application.DTOs.UserDTOs.Admin;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Queries.GetAllAppUsers
{
    public class GetAllAppUsersQueryHandler : IRequestHandler<GetAllAppUsersQuery, List<ResultAppUserDTO>>
    {
        private readonly IReadRepository<AppUser> _readRepository;

        public GetAllAppUsersQueryHandler(IReadRepository<AppUser> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<ResultAppUserDTO>> Handle(GetAllAppUsersQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetAll()
                .Select(u => new ResultAppUserDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    EmailConfirmed = u.EmailConfirmed,
                    PhoneNumber = u.PhoneNumber,
                    TwoFactorEnabled = u.TwoFactorEnabled,
                    LockoutEnabled = u.LockoutEnabled,
                    CreatedAtUtc = u.CreatedAtUtc,
                    UpdatedAtUtc = u.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
