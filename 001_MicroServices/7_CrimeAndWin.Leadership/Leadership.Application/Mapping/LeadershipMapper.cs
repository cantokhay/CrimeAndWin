using Leadership.Application.DTOs.LeaderboardDTOs;
using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using Leadership.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Leadership.Application.Mapping;

[Mapper]
public partial class LeadershipMapper
{
    // Leaderboard
    public partial Leaderboard ToEntity(CreateLeaderboardDTO dto);

    [MapProperty(nameof(Leaderboard.Entries.Count), nameof(ResultLeaderboardDTO.EntryCount))]
    public partial ResultLeaderboardDTO ToResultDto(Leaderboard entity);

    // LeaderboardEntry
    [MapProperty(nameof(CreateLeaderboardEntryDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(CreateLeaderboardEntryDTO.Position), "Rank.Position")]
    public partial LeaderboardEntry ToEntity(CreateLeaderboardEntryDTO dto);

    [MapProperty(nameof(LeaderboardEntry.Rank.RankPoints), nameof(ResultLeaderboardEntryDTO.RankPoints))]
    [MapProperty(nameof(LeaderboardEntry.Rank.Position), nameof(ResultLeaderboardEntryDTO.Position))]
    public partial ResultLeaderboardEntryDTO ToResultDto(LeaderboardEntry entity);

    // Collection Mappings
    public partial IEnumerable<ResultLeaderboardDTO> ToResultDtoList(IEnumerable<Leaderboard> entities);
    public partial IEnumerable<ResultLeaderboardEntryDTO> ToResultDtoList(IEnumerable<LeaderboardEntry> entities);

    // ==============
    // ADMIN MAPPING
    // ==============

    [MapperIgnoreTarget(nameof(Leaderboard.Entries))]
    public partial AdminResultLeaderboardDTO ToAdminResultDto(Leaderboard entity);

    [MapperIgnoreTarget(nameof(Leaderboard.Id))]
    [MapperIgnoreTarget(nameof(Leaderboard.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Leaderboard.UpdatedAtUtc))]
    [MapperIgnoreTarget(nameof(Leaderboard.IsDeleted))]
    [MapperIgnoreTarget(nameof(Leaderboard.Entries))]
    public partial Leaderboard ToEntity(AdminCreateLeaderboardDTO dto);

    [MapperIgnoreTarget(nameof(Leaderboard.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(Leaderboard.Entries))]
    public partial Leaderboard ToEntity(AdminUpdateLeaderboardDTO dto);

    [MapProperty(nameof(LeaderboardEntry.Rank.RankPoints), nameof(AdminResultLeaderboardEntryDTO.RankPoints))]
    [MapProperty(nameof(LeaderboardEntry.Rank.Position), nameof(AdminResultLeaderboardEntryDTO.Position))]
    public partial AdminResultLeaderboardEntryDTO ToAdminResultDto(LeaderboardEntry entity);

    [MapProperty(nameof(AdminResultLeaderboardEntryDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(AdminResultLeaderboardEntryDTO.Position), "Rank.Position")]
    public partial LeaderboardEntry ToEntity(AdminResultLeaderboardEntryDTO dto);

    [MapperIgnoreTarget(nameof(LeaderboardEntry.Id))]
    [MapperIgnoreTarget(nameof(LeaderboardEntry.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(LeaderboardEntry.UpdatedAtUtc))]
    [MapperIgnoreTarget(nameof(LeaderboardEntry.IsDeleted))]
    [MapperIgnoreTarget(nameof(LeaderboardEntry.Leaderboard))]
    [MapProperty(nameof(AdminCreateLeaderboardEntryDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(AdminCreateLeaderboardEntryDTO.Position), "Rank.Position")]
    public partial LeaderboardEntry ToEntity(AdminCreateLeaderboardEntryDTO dto);

    [MapperIgnoreTarget(nameof(LeaderboardEntry.CreatedAtUtc))]
    [MapperIgnoreTarget(nameof(LeaderboardEntry.Leaderboard))]
    [MapProperty(nameof(AdminUpdateLeaderboardEntryDTO.RankPoints), "Rank.RankPoints")]
    [MapProperty(nameof(AdminUpdateLeaderboardEntryDTO.Position), "Rank.Position")]
    public partial LeaderboardEntry ToEntity(AdminUpdateLeaderboardEntryDTO dto);

    // Admin Collection Mappings
    public partial IEnumerable<AdminResultLeaderboardDTO> ToAdminResultDtoList(IEnumerable<Leaderboard> entities);
    public partial IEnumerable<AdminResultLeaderboardEntryDTO> ToAdminResultDtoList(IEnumerable<LeaderboardEntry> entities);
}


