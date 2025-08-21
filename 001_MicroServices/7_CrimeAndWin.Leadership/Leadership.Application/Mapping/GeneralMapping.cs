using AutoMapper;
using Leadership.Application.DTOs.LeaderboardDTOs;
using Leadership.Application.DTOs.LeaderboardEntryDTOs;
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
        }
    }
}
