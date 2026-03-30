using Identity.Application.DTOs.RefreshTokenDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.RefreshToken.Queries.GetRefreshTokenById
{
    public class GetRefreshTokenByIdQueryHandler : IRequestHandler<GetRefreshTokenByIdQuery, ResultRefreshTokenDTO>
    {
        private readonly IReadRepository<Domain.Entities.RefreshToken> _readRepository;

        public GetRefreshTokenByIdQueryHandler(IReadRepository<Domain.Entities.RefreshToken> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<ResultRefreshTokenDTO> Handle(GetRefreshTokenByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepository.GetByIdAsync(request.id.ToString());
            if (entity == null) return null!;

            return new ResultRefreshTokenDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Token = entity.Token,
                ExpiresAtUtc = entity.ExpiresAtUtc,
                RevokedAtUtc = entity.RevokedAtUtc,
                ReplacedByToken = entity.ReplacedByToken,
                CreatedAtUtc = entity.CreatedAtUtc,
                UpdatedAtUtc = entity.UpdatedAtUtc
            };
        }
    }
}
