using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminCreateAction
{
    public sealed record AdminCreateActionDefinitionCommand(AdminCreateActionDefinitionDTO createActionDefinitionDTO) : IRequest<Guid>;
}

