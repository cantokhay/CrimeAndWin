using Identity.Application.DTOs.UserTokenDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserToken.Queries.GetUserTokenById
{
    public class GetUserTokenByIdQueryHandler : IRequestHandler<GetUserTokenByIdQuery, ResultUserTokenDTO>
    {
        private readonly IReadRepository<Domain.Entities.UserToken> _readRepository;

        public GetUserTokenByIdQueryHandler(IReadRepository<Domain.Entities.UserToken> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<ResultUserTokenDTO> Handle(GetUserTokenByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepository.GetByIdAsync(request.id.ToString());
            if (entity == null) return null!;

            return new ResultUserTokenDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                LoginProvider = entity.LoginProvider,
                Name = entity.Name,
                Value = entity.Value,
                CreatedAtUtc = entity.CreatedAtUtc,
                UpdatedAtUtc = entity.UpdatedAtUtc
            };
        }
    }
}
