using AutoMapper;
using GameWorld.Application.DTOs.GameWorldDTOs;
using GameWorld.Application.DTOs.SeasonDTOs;
using GameWorld.Domain.Entities;
namespace GameWorld.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.GameWorld, CreateGameWorldDTO>()
                .ForMember(d => d.MaxEnergy, m => m.MapFrom(s => s.Rule.MaxEnergy))
                .ForMember(d => d.RegenRatePerHour, m => m.MapFrom(s => s.Rule.RegenRatePerHour))
                .ForMember(d => d.Seasons, m => m.MapFrom(s => s.Seasons))
                .ReverseMap();

            CreateMap<Season, CreateSeasonDTO>()
                .ForMember(d => d.StartUtc, m => m.MapFrom(s => s.DateRange.StartUtc))
                .ForMember(d => d.EndUtc, m => m.MapFrom(s => s.DateRange.EndUtc))
                .ReverseMap();

            CreateMap<Domain.Entities.GameWorld, ResultGameWorldDTO>()
                .ForMember(d => d.MaxEnergy, m => m.MapFrom(s => s.Rule.MaxEnergy))
                .ForMember(d => d.RegenRatePerHour, m => m.MapFrom(s => s.Rule.RegenRatePerHour))
                .ForMember(d => d.Seasons, m => m.MapFrom(s => s.Seasons))
                .ReverseMap();

            CreateMap<Season, ResultSeasonDTO>()
                .ForMember(d => d.StartUtc, m => m.MapFrom(s => s.DateRange.StartUtc))
                .ForMember(d => d.EndUtc, m => m.MapFrom(s => s.DateRange.EndUtc))
                .ReverseMap();

            CreateMap<Domain.Entities.GameWorld, UpdateGameWorldDTO>()
                .ForMember(d => d.MaxEnergy, m => m.MapFrom(s => s.Rule.MaxEnergy))
                .ForMember(d => d.RegenRatePerHour, m => m.MapFrom(s => s.Rule.RegenRatePerHour))
                .ForMember(d => d.Seasons, m => m.MapFrom(s => s.Seasons))
                .ReverseMap();

            CreateMap<Season, UpdateSeasonDTO>()
                .ForMember(d => d.StartUtc, m => m.MapFrom(s => s.DateRange.StartUtc))
                .ForMember(d => d.EndUtc, m => m.MapFrom(s => s.DateRange.EndUtc))
                .ReverseMap();
        }
    }
}
