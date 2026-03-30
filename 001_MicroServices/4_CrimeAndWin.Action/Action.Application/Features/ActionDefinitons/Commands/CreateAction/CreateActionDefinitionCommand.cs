using Action.Application.DTOs.ActionDefinitionDTOs;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Commands.CreateAction
{
    public sealed record CreateActionDefinitionCommand(CreateActionDefinitionDTO Request)
        : IRequest<CreateActionDefinitionDTO>;
}
