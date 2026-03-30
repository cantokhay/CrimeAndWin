using AutoMapper;
using Leadership.Application.DTOs.LeaderboardDTOs;
using Leadership.Application.DTOs.LeaderboardDTOs.Admin;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;
using Leadership.Domain.Entities;
using Leadership.Domain.VOs;

namespace Leadership.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateLeaderboardDTO, Leaderboard>();


            CreateMap<Leaderboard, ResultLeaderboardDTO>()
            .ForMember(d => d.EntryCount, o => o.MapFrom(s => s.Entries.Count));


            CreateMap<CreateLeaderboardEntryDTO, LeaderboardEntry>()
            .ForMember(d => d.Rank, o => o.MapFrom(s => new Rank { RankPoints = s.RankPoints, Position = s.Position }));


            CreateMap<LeaderboardEntry, ResultLeaderboardEntryDTO>()
            .ForMember(d => d.RankPoints, o => o.MapFrom(s => s.Rank.RankPoints))
            .ForMember(d => d.Position, o => o.MapFrom(s => s.Rank.Position));

            // ==============
            // ADMIN MAPPING
            // ==============

            // Leaderboard
            CreateMap<Leaderboard, AdminResultLeaderboardDTO>()
                .ForMember(d => d.Entries, o => o.Ignore())
                .ReverseMap();

            CreateMap<AdminCreateLeaderboardDTO, Leaderboard>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.UpdatedAtUtc, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.Entries, o => o.Ignore());

            CreateMap<AdminUpdateLeaderboardDTO, Leaderboard>()
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.Entries, o => o.Ignore());

            // LeaderboardEntry
            CreateMap<LeaderboardEntry, AdminResultLeaderboardEntryDTO>()
                .ForMember(d => d.RankPoints, o => o.MapFrom(s => s.Rank.RankPoints))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.Rank.Position))
                .ReverseMap();

            CreateMap<AdminCreateLeaderboardEntryDTO, LeaderboardEntry>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Rank,
                    o => o.MapFrom(s => new Rank
                    {
                        RankPoints = s.RankPoints,
                        Position = s.Position
                    }))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.UpdatedAtUtc, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.Leaderboard, o => o.Ignore());

            CreateMap<AdminUpdateLeaderboardEntryDTO, LeaderboardEntry>()
                .ForMember(d => d.Rank,
                    o => o.MapFrom(s => new Rank
                    {
                        RankPoints = s.RankPoints,
                        Position = s.Position
                    }))
                .ForMember(d => d.CreatedAtUtc, o => o.Ignore())
                .ForMember(d => d.Leaderboard, o => o.Ignore());
        }
    }
}