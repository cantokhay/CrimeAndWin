using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserRole.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserRole> _writeRepository;

        public DeleteUserRoleCommandHandler(IWriteRepository<Domain.Entities.UserRole> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _writeRepository.RemoveAsync(request.id.ToString());
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
