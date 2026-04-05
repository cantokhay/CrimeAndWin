using Shared.Application.Abstractions.Messaging;

namespace Identity.Application.Features.User.Commands.ConfirmEmail
{
    public sealed record ConfirmEmailCommand(string Email, string Token) : IRequest<bool>;
}
