using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Queries.GetActionDefinitionByIdAsAdmin
{
    public sealed record GetActionDefinitionByIdAsAdminQuery(Guid id) : IRequest<AdminResultActionDefinitionDTO?>;
}
