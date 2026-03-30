using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserToken.Commands.DeleteUserToken
{
    public class DeleteUserTokenCommandHandler : IRequestHandler<DeleteUserTokenCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserToken> _writeRepository;

        public DeleteUserTokenCommandHandler(IWriteRepository<Domain.Entities.UserToken> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteUserTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _writeRepository.RemoveAsync(request.id.ToString());
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
