using Identity.Domain.Entities;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;

namespace Identity.Application.Features.User.Commands.ApproveUser
{
    public sealed class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, bool>
    {
        private readonly IWriteRepository<AppUser> _userWrite;
        private readonly IReadRepository<AppUser> _userRead;

        public ApproveUserCommandHandler(IWriteRepository<AppUser> userWrite, IReadRepository<AppUser> userRead)
        {
            _userWrite = userWrite;
            _userRead = userRead;
        }

        public async Task<bool> Handle(ApproveUserCommand request, CancellationToken ct)
        {
            var user = await _userRead.GetByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                return false;
            }

            user.IsApproved = true;

            _userWrite.Update(user);
            await _userWrite.SaveAsync();

            return true;
        }
    }
}
