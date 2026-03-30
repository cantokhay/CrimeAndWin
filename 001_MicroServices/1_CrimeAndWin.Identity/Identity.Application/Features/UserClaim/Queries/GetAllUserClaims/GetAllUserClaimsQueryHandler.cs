using Identity.Application.DTOs.UserClaimDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserClaim.Queries.GetAllUserClaims
{
    public class GetAllUserClaimsQueryHandler : IRequestHandler<GetAllUserClaimsQuery, List<ResultUserClaimDTO>>
    {
        private readonly IReadRepository<Domain.Entities.UserClaim> _readRepository;

        public GetAllUserClaimsQueryHandler(IReadRepository<Domain.Entities.UserClaim> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<ResultUserClaimDTO>> Handle(GetAllUserClaimsQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetAll()
                .Select(x => new ResultUserClaimDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue,
                    CreatedAtUtc = x.CreatedAtUtc,
                    UpdatedAtUtc = x.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
