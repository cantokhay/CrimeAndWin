using MediatR;
using Shared.Domain.Repository;

namespace GameWorld.Application.Features.GameWorld.Commands.DeleteGameWorld
{
    public sealed class DeleteGameWorldCommandHandler : IRequestHandler<DeleteGameWorldCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.GameWorld> _write;

        public DeleteGameWorldCommandHandler(IWriteRepository<Domain.Entities.GameWorld> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(DeleteGameWorldCommand request, CancellationToken cancellationToken)
        {
            var result = await _write.RemoveAsync(request.Id.ToString());
            await _write.SaveAsync();
            return result;
        }
    }
}
