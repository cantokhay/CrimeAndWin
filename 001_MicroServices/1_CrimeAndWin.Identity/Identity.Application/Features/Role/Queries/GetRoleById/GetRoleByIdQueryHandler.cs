using Identity.Application.DTOs.RoleDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.Role.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, ResultRoleDTO>
    {
        private readonly IReadRepository<Domain.Entities.Role> _readRepository;

        public GetRoleByIdQueryHandler(IReadRepository<Domain.Entities.Role> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<ResultRoleDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _readRepository.GetByIdAsync(request.id.ToString());
            if (role == null) return null!;

            return new ResultRoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName,
                Description = role.Description,
                CreatedAtUtc = role.CreatedAtUtc,
                UpdatedAtUtc = role.UpdatedAtUtc
            };
        }
    }
}
