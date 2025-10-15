using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Identity.Application.Features.RefreshToken.Queries.GetAllRefreshTokens
{
    public class GetAllRefreshTokensQueryHandler : IRequestHandler<GetAllRefreshTokensQuery, List<ResultRefreshTokenDTO>>
    {
        private readonly IReadRepository<Domain.Entities.RefreshToken> _readRepository;

        public GetAllRefreshTokensQueryHandler(IReadRepository<Domain.Entities.RefreshToken> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<ResultRefreshTokenDTO>> Handle(GetAllRefreshTokensQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetAll()
                .Select(x => new ResultRefreshTokenDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Token = x.Token,
                    ExpiresAtUtc = x.ExpiresAtUtc,
                    RevokedAtUtc = x.RevokedAtUtc,
                    ReplacedByToken = x.ReplacedByToken,
                    CreatedAtUtc = x.CreatedAtUtc,
                    UpdatedAtUtc = x.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
