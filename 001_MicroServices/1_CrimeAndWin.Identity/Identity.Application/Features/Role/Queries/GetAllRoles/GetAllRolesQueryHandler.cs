using Identity.Application.DTOs.RoleDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Identity.Application.Features.Role.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<ResultRoleDTO>>
    {
        private readonly IReadRepository<Domain.Entities.Role> _readRepository;

        public GetAllRolesQueryHandler(IReadRepository<Domain.Entities.Role> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<ResultRoleDTO>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetAll()
                .Select(r => new ResultRoleDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    NormalizedName = r.NormalizedName,
                    Description = r.Description,
                    CreatedAtUtc = r.CreatedAtUtc,
                    UpdatedAtUtc = r.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
