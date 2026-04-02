using Inventory.Application.DTOs.ItemDTOs.Admin;
using Mediator;

namespace Inventory.Application.Features.Item.Queries.GetAllItemsAsAdmin
{
    public sealed record GetAllItemsAsAdminQuery() : IRequest<List<AdminResultItemDTO>>;
}

