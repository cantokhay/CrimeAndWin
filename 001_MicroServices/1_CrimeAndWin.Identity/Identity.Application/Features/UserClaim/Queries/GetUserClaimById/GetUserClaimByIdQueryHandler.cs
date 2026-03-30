using Identity.Application.DTOs.UserClaimDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserClaim.Queries.GetUserClaimById
{
    public class GetUserClaimByIdQueryHandler : IRequestHandler<GetUserClaimByIdQuery, ResultUserClaimDTO>
    {
        private readonly IReadRepository<Domain.Entities.UserClaim> _readRepository;

        public GetUserClaimByIdQueryHandler(IReadRepository<Domain.Entities.UserClaim> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<ResultUserClaimDTO> Handle(GetUserClaimByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepository.GetByIdAsync(request.id.ToString());
            if (entity == null) return null!;

            return new ResultUserClaimDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                ClaimType = entity.ClaimType,
                ClaimValue = entity.ClaimValue,
                CreatedAtUtc = entity.CreatedAtUtc,
                UpdatedAtUtc = entity.UpdatedAtUtc
            };
        }
    }
}
