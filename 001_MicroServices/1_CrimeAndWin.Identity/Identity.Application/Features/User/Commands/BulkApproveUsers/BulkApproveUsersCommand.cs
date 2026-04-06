using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Commands.BulkApproveUsers;

public sealed record BulkApproveUsersCommand() : ICommand<int>;
