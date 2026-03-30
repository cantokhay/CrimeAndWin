using Identity.Application.DTOs.UserLoginDTOs.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserLogin.Queries.GetAllUserLogins
{
    public class GetAllUserLoginsQueryHandler : IRequestHandler<GetAllUserLoginsQuery, List<ResultUserLoginDTO>>
    {
        private readonly IReadRepository<Domain.Entities.UserLogin> _readRepository;

        public GetAllUserLoginsQueryHandler(IReadRepository<Domain.Entities.UserLogin> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<List<ResultUserLoginDTO>> Handle(GetAllUserLoginsQuery request, CancellationToken cancellationToken)
        {
            return await _readRepository
                .GetAll()
                .Select(x => new ResultUserLoginDTO
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    LoginProvider = x.LoginProvider,
                    ProviderKey = x.ProviderKey,
                    ProviderDisplayName = x.ProviderDisplayName,
                    CreatedAtUtc = x.CreatedAtUtc,
                    UpdatedAtUtc = x.UpdatedAtUtc
                })
                .ToListAsync(cancellationToken);
        }
    }
}
