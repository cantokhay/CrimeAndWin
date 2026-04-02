using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using Mediator;

namespace Action.Application.Features.ActionDefinitons.Queries.GetActionDefinitionByIdAsAdmin
{
    public sealed record GetActionDefinitionByIdAsAdminQuery(Guid id) : IRequest<AdminResultActionDefinitionDTO?>;
}

