using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.ActionDefinitons.Commands.AdminCreateAction
{
    public sealed record AdminCreateActionDefinitionCommand(AdminCreateActionDefinitionDTO createActionDefinitionDTO) : IRequest<Guid>;
}



