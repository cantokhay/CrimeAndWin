using Identity.Application.DTOs.UserLoginDTOs.Admin;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserLogin.Queries.GetUserLoginById
{
    public class GetUserLoginByIdQueryHandler : IRequestHandler<GetUserLoginByIdQuery, ResultUserLoginDTO>
    {
        private readonly IReadRepository<Domain.Entities.UserLogin> _readRepository;

        public GetUserLoginByIdQueryHandler(IReadRepository<Domain.Entities.UserLogin> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<ResultUserLoginDTO> Handle(GetUserLoginByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _readRepository.GetByIdAsync(request.id.ToString());
            if (entity == null) return null!;

            return new ResultUserLoginDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                LoginProvider = entity.LoginProvider,
                ProviderKey = entity.ProviderKey,
                ProviderDisplayName = entity.ProviderDisplayName,
                CreatedAtUtc = entity.CreatedAtUtc,
                UpdatedAtUtc = entity.UpdatedAtUtc
            };
        }
    }
}
