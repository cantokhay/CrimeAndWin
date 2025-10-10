using Action.Application.DTOs;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Commands.CreateAction
{
    public sealed record CreateActionDefinitionCommand(CreateActionDefinitionDTO Request)
        : IRequest<CreateActionDefinitionDTO>;
}
