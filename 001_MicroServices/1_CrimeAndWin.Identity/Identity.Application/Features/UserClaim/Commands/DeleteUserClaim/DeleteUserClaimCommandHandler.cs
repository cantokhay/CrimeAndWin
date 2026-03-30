using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserClaim.Commands.DeleteUserClaim
{
    public class DeleteUserClaimCommandHandler : IRequestHandler<DeleteUserClaimCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserClaim> _writeRepository;

        public DeleteUserClaimCommandHandler(IWriteRepository<Domain.Entities.UserClaim> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteUserClaimCommand request, CancellationToken cancellationToken)
        {
            var result = await _writeRepository.RemoveAsync(request.Id.ToString());
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
