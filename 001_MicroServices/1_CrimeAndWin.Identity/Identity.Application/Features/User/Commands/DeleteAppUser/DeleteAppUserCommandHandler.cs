using Identity.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Commands.DeleteAppUser
{
    public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand, bool>
    {
        private readonly IWriteRepository<AppUser> _writeRepository;

        public DeleteAppUserCommandHandler(IWriteRepository<AppUser> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _writeRepository.RemoveAsync(request.id.ToString());
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
