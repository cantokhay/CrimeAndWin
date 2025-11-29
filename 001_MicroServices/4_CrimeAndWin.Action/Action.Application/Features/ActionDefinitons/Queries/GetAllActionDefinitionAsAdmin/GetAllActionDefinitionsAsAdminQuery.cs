using Action.Application.DTOs.ActionDefinitionDTOs.Admin;
using MediatR;

namespace Action.Application.Features.ActionDefinitons.Queries.GetAllActionDefinitionAsAdmin
{
    public sealed record GetAllActionDefinitionsAsAdminQuery() : IRequest<List<AdminResultActionDefinitionDTO>>;
}
