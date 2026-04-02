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
    public partial ResultPlayerDTO ToResultDto(Player entity);

    [MapProperty(nameof(ResultPlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(ResultPlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(ResultPlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(ResultPlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(ResultPlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(ResultPlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(ResultPlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(ResultPlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(ResultPlayerDTO.RankPosition), "Rank.Position")]
    public partial Player ToEntity(ResultPlayerDTO dto);

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
    public partial Player ToEntity(CreatePlayerDTO dto);
    [MapProperty("Stats.Power", nameof(CreatePlayerDTO.Power))]
    [MapProperty("Stats.Defense", nameof(CreatePlayerDTO.Defense))]
    [MapProperty("Stats.Agility", nameof(CreatePlayerDTO.Agility))]
    [MapProperty("Stats.Luck", nameof(CreatePlayerDTO.Luck))]
    [MapProperty("Energy.Current", nameof(CreatePlayerDTO.EnergyCurrent))]
    [MapProperty("Energy.Max", nameof(CreatePlayerDTO.EnergyMax))]
    [MapProperty("Energy.RegenPerMinute", nameof(CreatePlayerDTO.EnergyRegenPerMinute))]
    [MapProperty("Rank.RankPoints", nameof(CreatePlayerDTO.RankPoints))]
    [MapProperty("Rank.Position", nameof(CreatePlayerDTO.RankPosition))]
    public partial CreatePlayerDTO ToCreateDto(Player entity);

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
    public partial Player ToEntity(UpdatePlayerDTO dto);

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
    public partial AdminResultPlayerDTO ToAdminResultDto(Player entity);

    [MapProperty(nameof(AdminCreatePlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminCreatePlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminCreatePlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(AdminCreatePlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(AdminCreatePlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(AdminCreatePlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(AdminCreatePlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(AdminCreatePlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(AdminCreatePlayerDTO.RankPosition), "Rank.Position")]
    public partial Player ToEntity(AdminCreatePlayerDTO dto);

    [MapProperty(nameof(AdminUpdatePlayerDTO.Power), "Stats.Power")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.Defense), "Stats.Defense")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.Agility), "Stats.Agility")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.Luck), "Stats.Luck")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.EnergyCurrent), "Energy.Current")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.EnergyMax), "Energy.Max")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.EnergyRegenPerMinute), "Energy.RegenPerMinute")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(AdminUpdatePlayerDTO.RankPosition), "Rank.Position")]
    public partial Player ToEntity(AdminUpdatePlayerDTO dto);

    // Collection Mappings
    public partial IEnumerable<ResultPlayerDTO> ToResultDtoList(IEnumerable<Player> entities);
    public partial IEnumerable<AdminResultPlayerDTO> ToAdminResultDtoList(IEnumerable<Player> entities);
}

