using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.RefreshToken.Commands.DeleteRefreshToken
{
    public class DeleteRefreshTokenCommandHandler : IRequestHandler<DeleteRefreshTokenCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.RefreshToken> _writeRepository;

        public DeleteRefreshTokenCommandHandler(IWriteRepository<Domain.Entities.RefreshToken> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _writeRepository.RemoveAsync(request.id.ToString());
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
