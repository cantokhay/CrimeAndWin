using Inventory.Application.DTOs.ItemDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Item.Queries.GetAllItemsAsAdmin
{
    public sealed record GetAllItemsAsAdminQuery() : IRequest<List<AdminResultItemDTO>>;
}


