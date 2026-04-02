using Inventory.Application.DTOs.InventoryDTOs;
using Inventory.Application.DTOs.InventoryDTOs.Admin;
using Inventory.Application.DTOs.ItemDTOs;
using Inventory.Application.DTOs.ItemDTOs.Admin;
using Inventory.Application.DTOs.VODTOs;
using Inventory.Domain.Entities;
using Inventory.Domain.VOs;
using Riok.Mapperly.Abstractions;

namespace Inventory.Application.Mapping;

[Mapper]
public partial class InventoryMapper
{
    // Item
    public partial ResultItemDTO ToResultDto(Item entity);
    public partial Item ToEntity(ResultItemDTO dto);

    // ItemStats
    public partial ResultItemStatsDTO ToResultDto(ItemStats entity);
    public partial ItemStats ToEntity(ResultItemStatsDTO dto);

    // ItemValue
    public partial ResultItemValueDTO ToResultDto(ItemValue entity);
    public partial ItemValue ToEntity(ResultItemValueDTO dto);

    // Inventory
    public partial ResultInventoryDTO ToResultDto(Domain.Entities.Inventory entity);
    public partial Domain.Entities.Inventory ToEntity(ResultInventoryDTO dto);

    // ==========================
    //        ADMIN MAPPINGS
    // ==========================

    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.Items))]
    public partial AdminResultInventoryDTO ToAdminResultDto(Domain.Entities.Inventory entity);
    public partial Domain.Entities.Inventory ToEntity(AdminResultInventoryDTO dto);

    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.Id))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.Items))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.UpdatedAtUtc))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.IsDeleted))]
    public partial Domain.Entities.Inventory ToEntity(AdminCreateInventoryDTO dto);

    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.Items))]
    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.CreatedAtUtc))]
    public partial Domain.Entities.Inventory ToEntity(AdminUpdateInventoryDTO dto);

    // Item Admin
    [MapProperty(nameof(Item.Stats.Damage), nameof(AdminResultItemDTO.Damage))]
    [MapProperty(nameof(Item.Stats.Defense), nameof(AdminResultItemDTO.Defense))]
    [MapProperty(nameof(Item.Stats.Power), nameof(AdminResultItemDTO.Power))]
    [MapProperty(nameof(Item.Value.Amount), nameof(AdminResultItemDTO.Amount))]
    [MapProperty(nameof(Item.Value.Currency), nameof(AdminResultItemDTO.Currency))]
    public partial AdminResultItemDTO ToAdminResultDto(Item entity);
    
    [MapProperty(nameof(AdminResultItemDTO.Damage), "Stats.Damage")]
    [MapProperty(nameof(AdminResultItemDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminResultItemDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminResultItemDTO.Amount), "Value.Amount")]
    [MapProperty(nameof(AdminResultItemDTO.Currency), "Value.Currency")]
    public partial Item ToEntity(AdminResultItemDTO dto);

    [MapperIgnoreTarget(nameof(Item.Id))]
    [MapperIgnoreTarget(nameof(Item.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Item.UpdatedAtUtc))]
    [MapperIgnoreTarget(nameof(Item.IsDeleted))]
    [MapperIgnoreTarget(nameof(Item.Inventory))]
    [MapProperty(nameof(AdminCreateItemDTO.Damage), "Stats.Damage")]
    [MapProperty(nameof(AdminCreateItemDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminCreateItemDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminCreateItemDTO.Amount), "Value.Amount")]
    [MapProperty(nameof(AdminCreateItemDTO.Currency), "Value.Currency")]
    public partial Item ToEntity(AdminCreateItemDTO dto);

    [MapperIgnoreTarget(nameof(Item.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Item.Inventory))]
    [MapProperty(nameof(AdminUpdateItemDTO.Damage), "Stats.Damage")]
    [MapProperty(nameof(AdminUpdateItemDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminUpdateItemDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminUpdateItemDTO.Amount), "Value.Amount")]
    [MapProperty(nameof(AdminUpdateItemDTO.Currency), "Value.Currency")]
    public partial Item ToEntity(AdminUpdateItemDTO dto);

    // Collection Mappings
    public partial IEnumerable<ResultItemDTO> ToResultDtoList(IEnumerable<Item> entities);
    public partial IEnumerable<ResultInventoryDTO> ToResultDtoList(IEnumerable<Domain.Entities.Inventory> entities);
    public partial IEnumerable<AdminResultItemDTO> ToAdminResultDtoList(IEnumerable<Item> entities);
    public partial IEnumerable<AdminResultInventoryDTO> ToAdminResultDtoList(IEnumerable<Domain.Entities.Inventory> entities);
}

