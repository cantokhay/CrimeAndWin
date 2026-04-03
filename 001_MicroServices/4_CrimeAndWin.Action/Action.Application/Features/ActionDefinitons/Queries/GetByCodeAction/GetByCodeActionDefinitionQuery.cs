using Action.Application.DTOs.ActionDefinitionDTOs;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.ActionDefinitons.Queries.GetByCodeAction
{
    public sealed record GetByCodeActionDefinitionQuery(string Code)
        : IRequest<ActionDefinitionDTO>;
}


