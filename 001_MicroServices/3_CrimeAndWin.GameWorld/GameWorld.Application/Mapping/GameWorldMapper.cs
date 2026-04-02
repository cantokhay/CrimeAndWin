using GameWorld.Application.DTOs.GameWorldDTOs;
using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace GameWorld.Application.Mapping;

[Mapper]
public partial class GameWorldMapper
{
    // GameWorld
    [MapProperty(nameof(Domain.Entities.GameWorld.Rule.MaxEnergy), nameof(ResultGameWorldDTO.MaxEnergy))]
    [MapProperty(nameof(Domain.Entities.GameWorld.Rule.RegenRatePerHour), nameof(ResultGameWorldDTO.RegenRatePerHour))]
    public partial ResultGameWorldDTO ToResultDto(Domain.Entities.GameWorld entity);

    [MapProperty(nameof(ResultGameWorldDTO.MaxEnergy), "Rule.MaxEnergy")]
    [MapProperty(nameof(ResultGameWorldDTO.RegenRatePerHour), "Rule.RegenRatePerHour")]
    public partial Domain.Entities.GameWorld ToEntity(ResultGameWorldDTO dto);

    [MapProperty(nameof(CreateGameWorldDTO.MaxEnergy), "Rule.MaxEnergy")]
    [MapProperty(nameof(CreateGameWorldDTO.RegenRatePerHour), "Rule.RegenRatePerHour")]
    public partial Domain.Entities.GameWorld ToEntity(CreateGameWorldDTO dto);
    [MapProperty("Rule.MaxEnergy", nameof(CreateGameWorldDTO.MaxEnergy))]
    [MapProperty("Rule.RegenRatePerHour", nameof(CreateGameWorldDTO.RegenRatePerHour))]
    public partial CreateGameWorldDTO ToCreateDto(Domain.Entities.GameWorld entity);

    [MapProperty(nameof(UpdateGameWorldDTO.MaxEnergy), "Rule.MaxEnergy")]
    [MapProperty(nameof(UpdateGameWorldDTO.RegenRatePerHour), "Rule.RegenRatePerHour")]
    public partial Domain.Entities.GameWorld ToEntity(UpdateGameWorldDTO dto);

    [MapProperty("Rule.MaxEnergy", nameof(UpdateGameWorldDTO.MaxEnergy))]
    [MapProperty("Rule.RegenRatePerHour", nameof(UpdateGameWorldDTO.RegenRatePerHour))]
    public partial UpdateGameWorldDTO ToUpdateDto(Domain.Entities.GameWorld entity);

    // Season
    [MapProperty(nameof(Domain.Entities.Season.DateRange.StartUtc), nameof(ResultSeasonDTO.StartUtc))]
    [MapProperty(nameof(Domain.Entities.Season.DateRange.EndUtc), nameof(ResultSeasonDTO.EndUtc))]
    public partial ResultSeasonDTO ToResultDto(Domain.Entities.Season entity);

    [MapProperty(nameof(ResultSeasonDTO.StartUtc), "DateRange.StartUtc")]
    [MapProperty(nameof(ResultSeasonDTO.EndUtc), "DateRange.EndUtc")]
    public partial Domain.Entities.Season ToEntity(ResultSeasonDTO dto);

    [MapProperty(nameof(CreateSeasonDTO.StartUtc), "DateRange.StartUtc")]
    [MapProperty(nameof(CreateSeasonDTO.EndUtc), "DateRange.EndUtc")]
    public partial Domain.Entities.Season ToEntity(CreateSeasonDTO dto);
    [MapProperty("DateRange.StartUtc", nameof(CreateSeasonDTO.StartUtc))]
    [MapProperty("DateRange.EndUtc", nameof(CreateSeasonDTO.EndUtc))]
    public partial CreateSeasonDTO ToCreateDto(Domain.Entities.Season entity);

    [MapProperty(nameof(UpdateSeasonDTO.StartUtc), "DateRange.StartUtc")]
    [MapProperty(nameof(UpdateSeasonDTO.EndUtc), "DateRange.EndUtc")]
    public partial Domain.Entities.Season ToEntity(UpdateSeasonDTO dto);

    [MapProperty("DateRange.StartUtc", nameof(UpdateSeasonDTO.StartUtc))]
    [MapProperty("DateRange.EndUtc", nameof(UpdateSeasonDTO.EndUtc))]
    public partial UpdateSeasonDTO ToUpdateDto(Domain.Entities.Season entity);

    // List Mappings
    public partial IEnumerable<ResultGameWorldDTO> ToResultDtoList(IEnumerable<Domain.Entities.GameWorld> entities);
    public partial IEnumerable<ResultSeasonDTO> ToResultDtoList(IEnumerable<Domain.Entities.Season> entities);
}


