using Inventory.Application.DTOs.ItemDTOs.Admin;
using Shared.Application.Abstractions.Messaging;

namespace Inventory.Application.Features.Item.Queries.GetItemByIdAsAdmin
{
    public sealed record GetItemByIdAsAdminQuery(Guid id) : IRequest<AdminResultItemDTO?>;
}


