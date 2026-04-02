using Action.Application.DTOs.ActionDefinitionDTOs;
using Mediator;

namespace Action.Application.Features.ActionDefinitons.Queries.GetByCodeAction
{
    public sealed record GetByCodeActionDefinitionQuery(string Code)
        : IRequest<ActionDefinitionDTO>;
}

