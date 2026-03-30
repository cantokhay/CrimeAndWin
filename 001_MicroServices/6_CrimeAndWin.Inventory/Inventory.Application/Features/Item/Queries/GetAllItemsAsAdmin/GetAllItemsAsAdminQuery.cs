using Inventory.Application.DTOs.ItemDTOs.Admin;
using MediatR;

namespace Inventory.Application.Features.Item.Queries.GetAllItemsAsAdmin
{
    public sealed record GetAllItemsAsAdminQuery() : IRequest<List<AdminResultItemDTO>>;
}
