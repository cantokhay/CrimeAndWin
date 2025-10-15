using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.Role> _writeRepository;

        public DeleteRoleCommandHandler(IWriteRepository<Domain.Entities.Role> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _writeRepository.RemoveAsync(request.id.ToString());
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
