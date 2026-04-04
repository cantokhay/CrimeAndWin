using GameWorld.Application.DTOs.GameWorldDTOs;
using GameWorld.Application.DTOs.SeasonDTOs;
using Riok.Mapperly.Abstractions;

namespace GameWorld.Application.Mapping;

[Mapper]
public partial class GameWorldMapper
{
    // GameWorld
    [MapProperty(nameof(Domain.Entities.GameWorld.Rule.MaxEnergy), nameof(ResultGameWorldDTO.MaxEnergy))]
    [MapProperty(nameof(Domain.Entities.GameWorld.Rule.RegenRatePerHour), nameof(ResultGameWorldDTO.RegenRatePerHour))]
    public static ResultGameWorldDTO? ToResultDto(Domain.Entities.GameWorld? entity)
    {
        if (entity == null) return null!;
        return new ResultGameWorldDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            MaxEnergy = entity.Rule?.MaxEnergy ?? 0,
            RegenRatePerHour = entity.Rule?.RegenRatePerHour ?? 0,
            Seasons = entity.Seasons != null
                ? ToResultDtoList(entity.Seasons).ToList()
                : new List<ResultSeasonDTO>()
        };
    }

    [MapProperty(nameof(ResultGameWorldDTO.MaxEnergy), "Rule.MaxEnergy")]
    [MapProperty(nameof(ResultGameWorldDTO.RegenRatePerHour), "Rule.RegenRatePerHour")]
    public static Domain.Entities.GameWorld? ToEntity(ResultGameWorldDTO? dto)
    {
        if (dto == null) return null!;
        return new Domain.Entities.GameWorld
        {
            Id = dto.Id,
            Name = dto.Name,
            Rule = new Domain.VOs.GameRule(dto.MaxEnergy, dto.RegenRatePerHour),
            Seasons = dto.Seasons != null
                ? ToEntity(dto.Seasons).ToList()
                : new List<Domain.Entities.Season>()
        };
    }

    [MapProperty(nameof(CreateGameWorldDTO.MaxEnergy), "Rule.MaxEnergy")]
    [MapProperty(nameof(CreateGameWorldDTO.RegenRatePerHour), "Rule.RegenRatePerHour")]
    public static Domain.Entities.GameWorld? ToEntity(CreateGameWorldDTO? dto)
    {
        if (dto == null) return null!;
        return new Domain.Entities.GameWorld
        {
            Name = dto.Name,
            Rule = new Domain.VOs.GameRule(dto.MaxEnergy, dto.RegenRatePerHour),
            Seasons = dto.Seasons != null
                ? ToEntity(dto.Seasons).ToList()
                : new List<Domain.Entities.Season>()
        };
    }

    [MapProperty("Rule.MaxEnergy", nameof(CreateGameWorldDTO.MaxEnergy))]
    [MapProperty("Rule.RegenRatePerHour", nameof(CreateGameWorldDTO.RegenRatePerHour))]
    public static CreateGameWorldDTO? ToCreateDto(Domain.Entities.GameWorld? entity)
    {
        if (entity == null) return null!;
        return new CreateGameWorldDTO
        {
            Name = entity.Name,
            MaxEnergy = entity.Rule?.MaxEnergy ?? 0,
            RegenRatePerHour = entity.Rule?.RegenRatePerHour ?? 0,
            Seasons = entity.Seasons != null
                ? ToResultDtoList(entity.Seasons).ToList()
                : new List<ResultSeasonDTO>()
        };
    }

    [MapProperty(nameof(UpdateGameWorldDTO.MaxEnergy), "Rule.MaxEnergy")]
    [MapProperty(nameof(UpdateGameWorldDTO.RegenRatePerHour), "Rule.RegenRatePerHour")]
    public static Domain.Entities.GameWorld? ToEntity(UpdateGameWorldDTO? dto)
    {
        if (dto == null) return null!;
        return new Domain.Entities.GameWorld
        {
            Id = dto.Id,
            Name = dto.Name,
            Rule = new Domain.VOs.GameRule(dto.MaxEnergy, dto.RegenRatePerHour),
            Seasons = dto.Seasons != null
                ? ToEntity(dto.Seasons).ToList()
                : new List<Domain.Entities.Season>()
        };
    }

    [MapProperty("Rule.MaxEnergy", nameof(UpdateGameWorldDTO.MaxEnergy))]
    [MapProperty("Rule.RegenRatePerHour", nameof(UpdateGameWorldDTO.RegenRatePerHour))]
    public static UpdateGameWorldDTO? ToUpdateDto(Domain.Entities.GameWorld? entity)
    {
        if (entity == null) return null!;
        return new UpdateGameWorldDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            MaxEnergy = entity.Rule?.MaxEnergy ?? 0,
            RegenRatePerHour = entity.Rule?.RegenRatePerHour ?? 0,
            Seasons = entity.Seasons != null
                ? ToResultDtoList(entity.Seasons).ToList()
                : new List<ResultSeasonDTO>()
        };
    }

    // Season
    [MapProperty(nameof(Domain.Entities.Season.DateRange.StartUtc), nameof(ResultSeasonDTO.StartUtc))]
    [MapProperty(nameof(Domain.Entities.Season.DateRange.EndUtc), nameof(ResultSeasonDTO.EndUtc))]
    public static ResultSeasonDTO? ToResultDto(Domain.Entities.Season? entity)
    {
        if (entity == null) return null!;
        return new ResultSeasonDTO
        {
            Id = entity.Id,
            GameWorldId = entity.GameWorldId,
            SeasonNumber = entity.SeasonNumber,
            StartUtc = entity.DateRange?.StartUtc ?? default,
            EndUtc = entity.DateRange?.EndUtc ?? default,
            IsActive = entity.IsActive
        };
    }

    [MapProperty(nameof(ResultSeasonDTO.StartUtc), "DateRange.StartUtc")]
    [MapProperty(nameof(ResultSeasonDTO.EndUtc), "DateRange.EndUtc")]
    public static Domain.Entities.Season? ToEntity(ResultSeasonDTO? dto)
    {
        if (dto == null) return null!;
        return new Domain.Entities.Season
        {
            Id = dto.Id,
            GameWorldId = dto.GameWorldId,
            SeasonNumber = dto.SeasonNumber,
            DateRange = new Domain.VOs.DateRange(dto.StartUtc, dto.EndUtc),
            IsActive = dto.IsActive
        };
    }

    [MapProperty(nameof(CreateSeasonDTO.StartUtc), "DateRange.StartUtc")]
    [MapProperty(nameof(CreateSeasonDTO.EndUtc), "DateRange.EndUtc")]
    public static Domain.Entities.Season? ToEntity(CreateSeasonDTO? dto)
    {
        if (dto == null) return null!;
        return new Domain.Entities.Season
        {
            SeasonNumber = dto.SeasonNumber,
            DateRange = new Domain.VOs.DateRange(dto.StartUtc, dto.EndUtc),
            IsActive = dto.IsActive
        };
    }

    [MapProperty("DateRange.StartUtc", nameof(CreateSeasonDTO.StartUtc))]
    [MapProperty("DateRange.EndUtc", nameof(CreateSeasonDTO.EndUtc))]
    public static CreateSeasonDTO? ToCreateDto(Domain.Entities.Season? entity)
    {
        if (entity == null) return null!;
        return new CreateSeasonDTO
        {
            SeasonNumber = entity.SeasonNumber,
            StartUtc = entity.DateRange?.StartUtc ?? default,
            EndUtc = entity.DateRange?.EndUtc ?? default,
            IsActive = entity.IsActive
        };
    }

    [MapProperty(nameof(UpdateSeasonDTO.StartUtc), "DateRange.StartUtc")]
    [MapProperty(nameof(UpdateSeasonDTO.EndUtc), "DateRange.EndUtc")]
    public static Domain.Entities.Season? ToEntity(UpdateSeasonDTO? dto)
    {
        if (dto == null) return null!;
        return new Domain.Entities.Season
        {
            Id = dto.Id,
            GameWorldId = dto.GameWorldId,
            SeasonNumber = dto.SeasonNumber,
            DateRange = new Domain.VOs.DateRange(dto.StartUtc, dto.EndUtc),
            IsActive = dto.IsActive
        };
    }

    [MapProperty("DateRange.StartUtc", nameof(UpdateSeasonDTO.StartUtc))]
    [MapProperty("DateRange.EndUtc", nameof(UpdateSeasonDTO.EndUtc))]
    public static UpdateSeasonDTO? ToUpdateDto(Domain.Entities.Season? entity)
    {
        if (entity == null) return null!;
        return new UpdateSeasonDTO
        {
            Id = entity.Id,
            GameWorldId = entity.GameWorldId,
            SeasonNumber = entity.SeasonNumber,
            StartUtc = entity.DateRange?.StartUtc ?? default,
            EndUtc = entity.DateRange?.EndUtc ?? default,
            IsActive = entity.IsActive
        };
    }

    // List Mappings
    public static IEnumerable<ResultGameWorldDTO> ToResultDtoList(IEnumerable<Domain.Entities.GameWorld>? entities)
    {
        if (entities == null) return Enumerable.Empty<ResultGameWorldDTO>();
        return entities.Select(e => ToResultDto(e)!);
    }

    public static IEnumerable<ResultSeasonDTO> ToResultDtoList(IEnumerable<Domain.Entities.Season>? entities)
    {
        if (entities == null) return Enumerable.Empty<ResultSeasonDTO>();
        return entities.Select(e => ToResultDto(e)!);
    }

    // Helper for mapping lists of seasons from DTOs to entities
    private static IEnumerable<Domain.Entities.Season> ToEntity(IEnumerable<ResultSeasonDTO>? dtos)
    {
        if (dtos == null) return Enumerable.Empty<Domain.Entities.Season>();
        return dtos.Select(d => ToEntity(d)!);
    }

    private static IEnumerable<Domain.Entities.Season> ToEntity(IEnumerable<CreateSeasonDTO>? dtos)
    {
        if (dtos == null) return Enumerable.Empty<Domain.Entities.Season>();
        return dtos.Select(d => ToEntity(d)!);
    }
}