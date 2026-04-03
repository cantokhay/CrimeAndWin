using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllActionDefinitionAsAdmin
{
    public sealed record GetAllActionDefinitionsAsAdminQuery() : IRequest<List<AdminResultActionDefinitionDTO>>;
}


