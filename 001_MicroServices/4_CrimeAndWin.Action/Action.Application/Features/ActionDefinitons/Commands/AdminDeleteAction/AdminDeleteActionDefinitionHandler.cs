using Action.Domain.Entities;
using Mediator;
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

        public async ValueTask<bool> Handle(AdminDeleteActionDefinitionCommand request, CancellationToken ct)
        {
            var ok = await _write.RemoveAsync(request.id.ToString());
            await _write.SaveAsync();
            return ok;
        }
    }
}

