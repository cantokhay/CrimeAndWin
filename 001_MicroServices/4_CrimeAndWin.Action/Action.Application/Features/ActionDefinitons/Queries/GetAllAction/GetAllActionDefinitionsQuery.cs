using Action.Application.DTOs;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllAction
{
    public sealed record GetAllActionDefinitionsQuery()
        : IRequest<List<ResultActionDefinitionDTO>>;
}
