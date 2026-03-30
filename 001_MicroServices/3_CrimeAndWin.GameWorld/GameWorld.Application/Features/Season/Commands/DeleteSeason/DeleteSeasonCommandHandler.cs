using MediatR;
using Shared.Domain.Repository;
using GameWorld.Application.Features.Season.Commands.DeleteSeason;

namespace Season.Application.Features.Season.Commands.DeleteSeason
{
    public sealed class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand, bool>
    {
        private readonly IWriteRepository<GameWorld.Domain.Entities.Season> _write;

        public DeleteSeasonCommandHandler(IWriteRepository<GameWorld.Domain.Entities.Season> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
        {
            var result = await _write.RemoveAsync(request.Id.ToString());
            await _write.SaveAsync();
            return result;
        }
    }
}
