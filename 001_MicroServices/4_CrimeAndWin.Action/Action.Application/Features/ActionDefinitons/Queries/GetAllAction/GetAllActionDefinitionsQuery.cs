using Action.Application.DTOs.ActionDefinitionDTOs;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllAction
{
    public sealed record GetAllActionDefinitionsQuery()
        : IRequest<List<ResultActionDefinitionDTO>>;
}
