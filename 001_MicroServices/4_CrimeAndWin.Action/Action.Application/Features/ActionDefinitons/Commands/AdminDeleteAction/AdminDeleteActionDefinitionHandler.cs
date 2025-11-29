using Action.Domain.Entities;
using MediatR;
using Shared.Domain.Repository;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminDeleteAction
{
    public sealed class AdminDeleteActionDefinitionHandler
            : IRequestHandler<AdminDeleteActionDefinitionCommand, bool>
    {
        private readonly IWriteRepository<ActionDefinition> _write;

        public AdminDeleteActionDefinitionHandler(IWriteRepository<ActionDefinition> write)
        {
            _write = write;
        }

        public async Task<bool> Handle(AdminDeleteActionDefinitionCommand request, CancellationToken ct)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}
