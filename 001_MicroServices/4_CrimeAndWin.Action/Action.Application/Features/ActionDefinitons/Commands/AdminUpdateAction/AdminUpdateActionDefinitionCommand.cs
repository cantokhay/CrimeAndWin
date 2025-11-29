using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminUpdateAction
{
    public sealed record AdminUpdateActionDefinitionCommand(AdminUpdateActionDefinitionDTO updateActionDefinitionDTO) : IRequest<bool>;
}
