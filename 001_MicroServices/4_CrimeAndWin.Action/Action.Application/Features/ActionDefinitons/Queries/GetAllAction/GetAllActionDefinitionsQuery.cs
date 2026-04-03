using Action.Application.DTOs.ActionDefinitionDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllAction
{
    public sealed record GetAllActionDefinitionsQuery()
        : IRequest<List<ResultActionDefinitionDTO>>;
}


