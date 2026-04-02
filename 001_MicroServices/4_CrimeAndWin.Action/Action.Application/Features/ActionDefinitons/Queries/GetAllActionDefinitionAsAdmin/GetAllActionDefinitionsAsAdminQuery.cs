using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Mediator;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllActionDefinitionAsAdmin
{
    public sealed record GetAllActionDefinitionsAsAdminQuery() : IRequest<List<AdminResultActionDefinitionDTO>>;
}

