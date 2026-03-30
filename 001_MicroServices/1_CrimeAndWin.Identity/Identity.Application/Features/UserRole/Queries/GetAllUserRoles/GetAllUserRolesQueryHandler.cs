using Identity.Application.DTOs.UserRoleDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserRole.Queries.GetAllUserRoles
{
    public class GetAllUserRolesQueryHandler : IRequestHandler<GetAllUserRolesQuery, List<ResultUserRoleDTO>>
    {
        private readonly IReadRepository<Domain.Entities.UserRole> _readRepository;

        public GetAllUserRolesQueryHandler(IReadRepository<Domain.Entities.UserRole> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<ResultUserRoleDTO>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetAll()
                .Select(x => new ResultUserRoleDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    RoleId = x.RoleId,
                    CreatedAtUtc = x.CreatedAtUtc,
                    UpdatedAtUtc = x.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
