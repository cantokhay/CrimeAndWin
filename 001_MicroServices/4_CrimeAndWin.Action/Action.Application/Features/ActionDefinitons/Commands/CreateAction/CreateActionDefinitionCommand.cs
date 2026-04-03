using Action.Application.DTOs.ActionDefinitionDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.ActionDefinitons.Commands.CreateAction
{
    public sealed record CreateActionDefinitionCommand(CreateActionDefinitionDTO Request)
        : IRequest<CreateActionDefinitionDTO>;
}


