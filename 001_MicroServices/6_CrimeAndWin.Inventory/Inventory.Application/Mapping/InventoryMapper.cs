using System.Linq;
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
    public ResultItemDTO ToResultDto(Item entity)
    {
        if (entity == null) return null;
        return new ResultItemDTO
        {
            Id = entity.Id,
            InventoryId = entity.InventoryId,
            Name = entity.Name,
            Quantity = entity.Quantity,
            Stats = ToResultDto(entity.Stats),
            Value = ToResultDto(entity.Value)
        };
    }

    public Item ToEntity(ResultItemDTO dto)
    {
        if (dto == null) return null;
        return new Item
        {
            Id = dto.Id,
            InventoryId = dto.InventoryId,
            Name = dto.Name,
            Quantity = dto.Quantity,
            Stats = ToEntity(dto.Stats),
            Value = ToEntity(dto.Value)
        };
    }

    // ItemStats
    public ResultItemStatsDTO ToResultDto(ItemStats entity)
    {
        return new ResultItemStatsDTO
        {
            Damage = entity.Damage,
            Defense = entity.Defense,
            Power = entity.Power
        };
    }

    public ItemStats ToEntity(ResultItemStatsDTO dto)
    {
        return new ItemStats
        {
            Damage = dto.Damage,
            Defense = dto.Defense,
            Power = dto.Power
        };
    }

    // ItemValue
    public ResultItemValueDTO ToResultDto(ItemValue entity)
    {
        return new ResultItemValueDTO
        {
            Amount = entity.Amount,
            Currency = entity.Currency
        };
    }

    public ItemValue ToEntity(ResultItemValueDTO dto)
    {
        return new ItemValue
        {
            Amount = dto.Amount,
            Currency = dto.Currency
        };
    }

    // Inventory
    public ResultInventoryDTO ToResultDto(Domain.Entities.Inventory entity)
    {
        if (entity == null) return null;
        return new ResultInventoryDTO
        {
            Id = entity.Id,
            PlayerId = entity.PlayerId,
            Items = entity.Items?.Select(ToResultDto).ToList()
        };
    }

    public Domain.Entities.Inventory ToEntity(ResultInventoryDTO dto)
    {
        if (dto == null) return null;
        return new Domain.Entities.Inventory
        {
            Id = dto.Id,
            PlayerId = dto.PlayerId,
            Items = dto.Items?.Select(ToEntity).ToList()
        };
    }

    // ==========================
    //        ADMIN MAPPINGS
    // ==========================

    [MapperIgnoreTarget(nameof(Domain.Entities.Inventory.Items))]
    public AdminResultInventoryDTO ToAdminResultDto(Domain.Entities.Inventory entity)
    {
        if (entity == null) return null;
        return new AdminResultInventoryDTO
        {
            Id = entity.Id,
            PlayerId = entity.PlayerId,
            Items = entity.Items?.Select(ToAdminResultDto).ToList(),
            CreatedAtUtc = entity.CreatedAtUtc,
            UpdatedAtUtc = entity.UpdatedAtUtc
        };
    }

    public Domain.Entities.Inventory ToEntity(AdminResultInventoryDTO dto)
    {
        if (dto == null) return null;
        return new Domain.Entities.Inventory
        {
            Id = dto.Id,
            PlayerId = dto.PlayerId,
            Items = dto.Items?.Select(ToEntity).ToList(),
            CreatedAtUtc = dto.CreatedAtUtc,
            UpdatedAtUtc = dto.UpdatedAtUtc
        };
    }

    public Domain.Entities.Inventory ToEntity(AdminCreateInventoryDTO dto)
    {
        if (dto == null) return null;
        return new Domain.Entities.Inventory
        {
            PlayerId = dto.PlayerId
        };
    }

    public Domain.Entities.Inventory ToEntity(AdminUpdateInventoryDTO dto)
    {
        if (dto == null) return null;
        return new Domain.Entities.Inventory
        {
            Id = dto.Id,
            PlayerId = dto.PlayerId
        };
    }

    // Item Admin
    [MapProperty(nameof(Item.Stats.Damage), nameof(AdminResultItemDTO.Damage))]
    [MapProperty(nameof(Item.Stats.Defense), nameof(AdminResultItemDTO.Defense))]
    [MapProperty(nameof(Item.Stats.Power), nameof(AdminResultItemDTO.Power))]
    [MapProperty(nameof(Item.Value.Amount), nameof(AdminResultItemDTO.Amount))]
    [MapProperty(nameof(Item.Value.Currency), nameof(AdminResultItemDTO.Currency))]
    public AdminResultItemDTO ToAdminResultDto(Item entity)
    {
        if (entity == null) return null;
        return new AdminResultItemDTO
        {
            Id = entity.Id,
            InventoryId = entity.InventoryId,
            Name = entity.Name,
            Quantity = entity.Quantity,
            Damage = entity.Stats.Damage,
            Defense = entity.Stats.Defense,
            Power = entity.Stats.Power,
            Amount = entity.Value.Amount,
            Currency = entity.Value.Currency,
            CreatedAtUtc = entity.CreatedAtUtc,
            UpdatedAtUtc = entity.UpdatedAtUtc
        };
    }

    [MapProperty(nameof(AdminResultItemDTO.Damage), "Stats.Damage")]
    [MapProperty(nameof(AdminResultItemDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminResultItemDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminResultItemDTO.Amount), "Value.Amount")]
    [MapProperty(nameof(AdminResultItemDTO.Currency), "Value.Currency")]
    public Item ToEntity(AdminResultItemDTO dto)
    {
        if (dto == null) return null;
        return new Item
        {
            Id = dto.Id,
            InventoryId = dto.InventoryId,
            Name = dto.Name,
            Quantity = dto.Quantity,
            Stats = new ItemStats
            {
                Damage = dto.Damage,
                Defense = dto.Defense,
                Power = dto.Power
            },
            Value = new ItemValue
            {
                Amount = dto.Amount,
                Currency = dto.Currency
            },
            CreatedAtUtc = dto.CreatedAtUtc,
            UpdatedAtUtc = dto.UpdatedAtUtc
        };
    }

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
    public Item ToEntity(AdminCreateItemDTO dto)
    {
        if (dto == null) return null;
        return new Item
        {
            InventoryId = dto.InventoryId,
            Name = dto.Name,
            Quantity = dto.Quantity,
            Stats = new ItemStats
            {
                Damage = dto.Damage,
                Defense = dto.Defense,
                Power = dto.Power
            },
            Value = new ItemValue
            {
                Amount = dto.Amount,
                Currency = dto.Currency
            }
        };
    }

    [MapperIgnoreTarget(nameof(Item.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Item.Inventory))]
    [MapProperty(nameof(AdminUpdateItemDTO.Damage), "Stats.Damage")]
    [MapProperty(nameof(AdminUpdateItemDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminUpdateItemDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminUpdateItemDTO.Amount), "Value.Amount")]
    [MapProperty(nameof(AdminUpdateItemDTO.Currency), "Value.Currency")]
    public Item ToEntity(AdminUpdateItemDTO dto)
    {
        if (dto == null) return null;
        return new Item
        {
            Id = dto.Id,
            InventoryId = dto.InventoryId,
            Name = dto.Name,
            Quantity = dto.Quantity,
            Stats = new ItemStats
            {
                Damage = dto.Damage,
                Defense = dto.Defense,
                Power = dto.Power
            },
            Value = new ItemValue
            {
                Amount = dto.Amount,
                Currency = dto.Currency
            }
        };
    }

    // Collection Mappings
    public IEnumerable<ResultItemDTO> ToResultDtoList(IEnumerable<Item> entities)
    {
        return entities?.Select(ToResultDto);
    }

    public IEnumerable<ResultInventoryDTO> ToResultDtoList(IEnumerable<Domain.Entities.Inventory> entities)
    {
        return entities?.Select(ToResultDto);
    }

    public IEnumerable<AdminResultItemDTO> ToAdminResultDtoList(IEnumerable<Item> entities)
    {
        return entities?.Select(ToAdminResultDto);
    }

    public IEnumerable<AdminResultInventoryDTO> ToAdminResultDtoList(IEnumerable<Domain.Entities.Inventory> entities)
    {
        return entities?.Select(ToAdminResultDto);
    }
}


