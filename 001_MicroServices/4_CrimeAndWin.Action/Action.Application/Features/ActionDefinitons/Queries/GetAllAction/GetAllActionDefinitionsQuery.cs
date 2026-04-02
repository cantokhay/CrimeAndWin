using Action.Application.DTOs.ActionDefinitionDTOs;
using Mediator;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllAction
{
    public sealed record GetAllActionDefinitionsQuery()
        : IRequest<List<ResultActionDefinitionDTO>>;
}

