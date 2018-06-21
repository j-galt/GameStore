using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.Web.ApiResources;

namespace GameStore.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to ApiResource.
            CreateMap<Game, GameGetResource>();

            // ApiResource to Domain.
            CreateMap<GameCreateResource, Game>()
                .ForMember(g => g.GameId, opt => opt.Ignore())
                .ForMember(g => g.Genres, opt => opt.MapFrom(gcr => gcr.Genres))
                .ForMember(g => g.PlatformTypes, opt => opt.MapFrom(gcr => gcr.PlatformTypes));

            CreateMap<CommentResource, Comment>()
                .ForMember(c => c.GameId, opt => opt.Ignore())
                .ForMember(c => c.CommentId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GenreResource, Genre>()
                .ForMember(g => g.ParentGenre, opt => opt.Ignore())
                .ForMember(g => g.Games, opt => opt.Ignore())
                .ForMember(g => g.ParentGenreName, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PlatformTypeResource, PlatformType>()              
                .ReverseMap();
        }
    }
}