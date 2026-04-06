using Identity.Domain.Entities;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.User.Commands.BulkApproveUsers;

public sealed class BulkApproveUsersCommandHandler : IRequestHandler<BulkApproveUsersCommand, int>
{
    private readonly IWriteRepository<AppUser> _userWrite;
    private readonly IReadRepository<AppUser> _userRead;

    public BulkApproveUsersCommandHandler(IWriteRepository<AppUser> userWrite, IReadRepository<AppUser> userRead)
    {
        _userWrite = userWrite;
        _userRead = userRead;
    }

    public async Task<int> Handle(BulkApproveUsersCommand request, CancellationToken ct)
    {
        // Optimization: ONLY fetch pending users, not the whole table
        var pending = await _userRead.GetWhere(u => !u.IsApproved).ToListAsync(ct);

        if (!pending.Any()) return 0;

        foreach (var user in pending)
        {
            user.IsApproved = true;
            _userWrite.Update(user);
        }

        await _userWrite.SaveAsync();
        return pending.Count;
    }
}
