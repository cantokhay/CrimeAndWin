using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Mediator;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminUpdateAction
{
    public sealed record AdminUpdateActionDefinitionCommand(AdminUpdateActionDefinitionDTO updateActionDefinitionDTO) : IRequest<bool>;
}

