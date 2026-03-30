using Identity.Application.DTOs.UserRoleDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserRole.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQuery, ResultUserRoleDTO>
    {
        private readonly IReadRepository<Domain.Entities.UserRole> _readRepository;

        public GetUserRoleByIdQueryHandler(IReadRepository<Domain.Entities.UserRole> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<ResultUserRoleDTO> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepository.GetByIdAsync(request.id.ToString());
            if (entity == null) return null!;

            return new ResultUserRoleDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                RoleId = entity.RoleId,
                CreatedAtUtc = entity.CreatedAtUtc,
                UpdatedAtUtc = entity.UpdatedAtUtc
            };
        }
    }
}
