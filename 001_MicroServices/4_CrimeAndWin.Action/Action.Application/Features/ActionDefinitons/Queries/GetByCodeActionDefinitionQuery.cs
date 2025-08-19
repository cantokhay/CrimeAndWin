using Action.Application.DTOs;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Queries
{
    public sealed record GetByCodeActionDefinitionQuery(string Code)
        : IRequest<ActionDefinitionDTO>;
}
