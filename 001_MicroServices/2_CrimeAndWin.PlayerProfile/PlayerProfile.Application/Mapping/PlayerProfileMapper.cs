using PlayerProfile.Application.DTOs.PlayerDTOs;
using PlayerProfile.Application.DTOs.PlayerDTOs.Admin;
using PlayerProfile.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace PlayerProfile.Application.Mapping;

[Mapper]
public partial class PlayerProfileMapper
{
    // Player
    [MapProperty(nameof(Player.Stats.Power), nameof(ResultPlayerDTO.Power))]
    [MapProperty(nameof(Player.Stats.Defense), nameof(ResultPlayerDTO.Defense))]
    [MapProperty(nameof(Player.Stats.Agility), nameof(ResultPlayerDTO.Agility))]
    [MapProperty(nameof(Player.Stats.Luck), nameof(ResultPlayerDTO.Luck))]
    [MapProperty(nameof(Player.Energy.Current), nameof(ResultPlayerDTO.EnergyCurrent))]
    [MapProperty(nameof(Player.Energy.Max), nameof(ResultPlayerDTO.EnergyCurrent))]
    [MapProperty("Energy.Max", nameof(ResultPlayerDTO.EnergyMax))]
    [MapProperty(nameof(Player.Energy.RegenPerMinute), nameof(ResultPlayerDTO.EnergyRegenPerMinute))]
    [MapProperty(nameof(Player.Rank.RankPoints), nameof(ResultPlayerDTO.RankPoints))]
    [MapProperty(nameof(Player.Rank.Position), nameof(ResultPlayerDTO.RankPosition))]
    public ResultPlayerDTO ToResultDto(Player entity)
    {
        if (entity == null) return null;
        return new ResultPlayerDTO
        {
            Id = entity.Id, 
            UserId = entity.AppUserId,
            DisplayName = entity.DisplayName,
            AvatarKey = entity.AvatarKey,
            Power = entity.Stats.Power,
            Defense = entity.Stats.Defense,
            Agility = entity.Stats.Agility,
            Luck = entity.Stats.Luck,
            EnergyCurrent = entity.Energy.Current,
            EnergyMax = entity.Energy.Max,
            EnergyRegenPerMinute = entity.Energy.RegenPerMinute,
            RankPoints = entity.Rank.RankPoints,
            RankPosition = entity.Rank.Position
        };
    }

    [MapProperty(nameof(ResultPlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(ResultPlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(ResultPlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(ResultPlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(ResultPlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(ResultPlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(ResultPlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(ResultPlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(ResultPlayerDTO.RankPosition), "Rank.Position")]
    public Player ToEntity(ResultPlayerDTO dto)
    {
        if (dto == null) return null;
        return new Player
        {
            Id = dto.Id,
            AppUserId = dto.UserId,
            DisplayName = dto.DisplayName,
            AvatarKey = dto.AvatarKey,
            Stats = new Domain.VOs.Stats
            {
                Power = dto.Power,
                Defense = dto.Defense,
                Agility = dto.Agility,
                Luck = dto.Luck
            },
            Energy = new Domain.VOs.Energy
            {
                Current = dto.EnergyCurrent,
                Max = dto.EnergyMax,
                RegenPerMinute = dto.EnergyRegenPerMinute
            },
            Rank = new Domain.VOs.Rank
            {
                RankPoints = dto.RankPoints,
                Position = dto.RankPosition
            }
        };
    }

    // CreatePlayerDTO
    [MapProperty(nameof(CreatePlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(CreatePlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(CreatePlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(CreatePlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(CreatePlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(CreatePlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(CreatePlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(CreatePlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(CreatePlayerDTO.RankPosition), "Rank.Position")]
    public Player ToEntity(CreatePlayerDTO dto)
    {
        if (dto == null) return null;
        return new Player
        {
            AppUserId = dto.UserId,
            DisplayName = dto.DisplayName,
            AvatarKey = dto.AvatarKey,
            Stats = new Domain.VOs.Stats
            {
                Power = dto.Power,
                Defense = dto.Defense,
                Agility = dto.Agility,
                Luck = dto.Luck
            },
            Energy = new Domain.VOs.Energy
            {
                Current = dto.EnergyCurrent,
                Max = dto.EnergyMax,
                RegenPerMinute = dto.EnergyRegenPerMinute
            },
            Rank = new Domain.VOs.Rank
            {
                RankPoints = dto.RankPoints,
                Position = dto.RankPosition
            }
        };
    }

    [MapProperty("Stats.Power", nameof(CreatePlayerDTO.Power))]
    [MapProperty("Stats.Defense", nameof(CreatePlayerDTO.Defense))]
    [MapProperty("Stats.Agility", nameof(CreatePlayerDTO.Agility))]
    [MapProperty("Stats.Luck", nameof(CreatePlayerDTO.Luck))]
    [MapProperty("Energy.Current", nameof(CreatePlayerDTO.EnergyCurrent))]
    [MapProperty("Energy.Max", nameof(CreatePlayerDTO.EnergyMax))]
    [MapProperty("Energy.RegenPerMinute", nameof(CreatePlayerDTO.EnergyRegenPerMinute))]
    [MapProperty("Rank.RankPoints", nameof(CreatePlayerDTO.RankPoints))]
    [MapProperty("Rank.Position", nameof(CreatePlayerDTO.RankPosition))]
    public CreatePlayerDTO ToCreateDto(Player entity)
    {
        if (entity == null) return null;
        return new CreatePlayerDTO
        {
            UserId = entity.AppUserId,
            DisplayName = entity.DisplayName,
            AvatarKey = entity.AvatarKey,
            Power = entity.Stats.Power,
            Defense = entity.Stats.Defense,
            Agility = entity.Stats.Agility,
            Luck = entity.Stats.Luck,
            EnergyCurrent = entity.Energy.Current,
            EnergyMax = entity.Energy.Max,
            EnergyRegenPerMinute = entity.Energy.RegenPerMinute,
            RankPoints = entity.Rank.RankPoints,
            RankPosition = entity.Rank.Position
        };
    }

    // UpdatePlayerDTO
    [MapProperty(nameof(UpdatePlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(UpdatePlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(UpdatePlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(UpdatePlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(UpdatePlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(UpdatePlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(UpdatePlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(UpdatePlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(UpdatePlayerDTO.RankPosition), "Rank.Position")]
    public Player ToEntity(UpdatePlayerDTO dto)
    {
        if (dto == null) return null;
        return new Player
        {
            Id = dto.Id,
            AppUserId = dto.UserId,
            DisplayName = dto.DisplayName,
            AvatarKey = dto.AvatarKey,
            Stats = new Domain.VOs.Stats
            {
                Power = dto.Power,
                Defense = dto.Defense,
                Agility = dto.Agility,
                Luck = dto.Luck
            },
            Energy = new Domain.VOs.Energy
            {
                Current = dto.EnergyCurrent,
                Max = dto.EnergyMax,
                RegenPerMinute = dto.EnergyRegenPerMinute
            },
            Rank = new Domain.VOs.Rank
            {
                RankPoints = dto.RankPoints,
                Position = dto.RankPosition
            },
            CreatedAtUtc = dto.CreatedAtUtc
        };
    }

    // Admin Mappings
    [MapProperty(nameof(Player.Stats.Power), nameof(AdminResultPlayerDTO.Power))]
    [MapProperty(nameof(Player.Stats.Defense), nameof(AdminResultPlayerDTO.Defense))]
    [MapProperty(nameof(Player.Stats.Agility), nameof(AdminResultPlayerDTO.Agility))]
    [MapProperty(nameof(Player.Stats.Luck), nameof(AdminResultPlayerDTO.Luck))]
    [MapProperty(nameof(Player.Energy.Current), nameof(AdminResultPlayerDTO.EnergyCurrent))]
    [MapProperty(nameof(Player.Energy.Max), nameof(AdminResultPlayerDTO.EnergyMax))]
    [MapProperty(nameof(Player.Energy.RegenPerMinute), nameof(AdminResultPlayerDTO.EnergyRegenPerMinute))]
    [MapProperty(nameof(Player.Rank.RankPoints), nameof(AdminResultPlayerDTO.RankPoints))]
    [MapProperty(nameof(Player.Rank.Position), nameof(AdminResultPlayerDTO.RankPosition))]
    public AdminResultPlayerDTO ToAdminResultDto(Player entity)
    {
        if (entity == null) return null;
        return new AdminResultPlayerDTO
        {
            Id = entity.Id,
            AppUserId = entity.AppUserId,
            DisplayName = entity.DisplayName,
            AvatarKey = entity.AvatarKey,
            Power = entity.Stats.Power,
            Defense = entity.Stats.Defense,
            Agility = entity.Stats.Agility,
            Luck = entity.Stats.Luck,
            EnergyCurrent = entity.Energy.Current,
            EnergyMax = entity.Energy.Max,
            EnergyRegenPerMinute = entity.Energy.RegenPerMinute,
            RankPoints = entity.Rank.RankPoints,
            RankPosition = entity.Rank.Position,
            LastEnergyCalcUtc = entity.LastEnergyCalcUtc,
            CreatedAtUtc = entity.CreatedAtUtc,
            UpdatedAtUtc = entity.UpdatedAtUtc
        };
    }

    [MapProperty(nameof(AdminCreatePlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminCreatePlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminCreatePlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(AdminCreatePlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(AdminCreatePlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(AdminCreatePlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(AdminCreatePlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(AdminCreatePlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(AdminCreatePlayerDTO.RankPosition), "Rank.Position")]
    public Player ToEntity(AdminCreatePlayerDTO dto)
    {
        if (dto == null) return null;
        return new Player
        {
            AppUserId = dto.AppUserId,
            DisplayName = dto.DisplayName,
            AvatarKey = dto.AvatarKey,
            Stats = new Domain.VOs.Stats
            {
                Power = dto.Power,
                Defense = dto.Defense,
                Agility = dto.Agility,
                Luck = dto.Luck
            },
            Energy = new Domain.VOs.Energy
            {
                Current = dto.EnergyCurrent,
                Max = dto.EnergyMax,
                RegenPerMinute = dto.EnergyRegenPerMinute
            },
            Rank = new Domain.VOs.Rank
            {
                RankPoints = dto.RankPoints,
                Position = dto.RankPosition
            },
            LastEnergyCalcUtc = dto.LastEnergyCalcUtc
        };
    }

    [MapProperty(nameof(AdminUpdatePlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.RankPosition), "Rank.Position")]
    public Player ToEntity(AdminUpdatePlayerDTO dto)
    {
        if (dto == null) return null;
        return new Player
        {
            Id = dto.Id,
            AppUserId = dto.AppUserId,
            DisplayName = dto.DisplayName,
            AvatarKey = dto.AvatarKey,
            Stats = new Domain.VOs.Stats
            {
                Power = dto.Power,
                Defense = dto.Defense,
                Agility = dto.Agility,
                Luck = dto.Luck
            },
            Energy = new Domain.VOs.Energy
            {
                Current = dto.EnergyCurrent,
                Max = dto.EnergyMax,
                RegenPerMinute = dto.EnergyRegenPerMinute
            },
            Rank = new Domain.VOs.Rank
            {
                RankPoints = dto.RankPoints,
                Position = dto.RankPosition
            },
            LastEnergyCalcUtc = dto.LastEnergyCalcUtc
        };
    }

    // Collection Mappings
    public IEnumerable<ResultPlayerDTO> ToResultDtoList(IEnumerable<Player> entities)
    {
        return entities?.Select(ToResultDto);
    }

    public IEnumerable<AdminResultPlayerDTO> ToAdminResultDtoList(IEnumerable<Player> entities)
    {
        return entities?.Select(ToAdminResultDto);
    }
}

