using MediatR;
using Shared.Domain.Repository;

namespace Identity.Application.Features.UserLogin.Commands.DeleteUserLogin
{
    public class DeleteUserLoginCommandHandler : IRequestHandler<DeleteUserLoginCommand, bool>
    {
        private readonly IWriteRepository<Domain.Entities.UserLogin> _writeRepository;

        public DeleteUserLoginCommandHandler(IWriteRepository<Domain.Entities.UserLogin> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteUserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _writeRepository.RemoveAsync(request.id.ToString());
            await _writeRepository.SaveAsync();
            return result;
        }
    }
}
