using Identity.Application.DTOs.UserTokenDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserToken.Queries.GetAllUserTokens
{
    public class GetAllUserTokensQueryHandler : IRequestHandler<GetAllUserTokensQuery, List<ResultUserTokenDTO>>
    {
        private readonly IReadRepository<Domain.Entities.UserToken> _readRepository;

        public GetAllUserTokensQueryHandler(IReadRepository<Domain.Entities.UserToken> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<ResultUserTokenDTO>> Handle(GetAllUserTokensQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetAll()
                .Select(x => new ResultUserTokenDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    LoginProvider = x.LoginProvider,
                    Name = x.Name,
                    Value = x.Value,
                    CreatedAtUtc = x.CreatedAtUtc,
                    UpdatedAtUtc = x.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
