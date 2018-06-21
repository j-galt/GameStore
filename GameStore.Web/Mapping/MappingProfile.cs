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
                //.ForMember(g => g., opt => opt.Ignore())
                .ForMember(g => g.Comments, opt => opt.Ignore())
                .ForMember(g => g.Publisher, opt => opt.Ignore())
                .ForMember(g => g.Genres, opt => opt.MapFrom(gcr => gcr.Genres))
                .ForMember(g => g.PlatformTypes, opt => opt.MapFrom(gcr => gcr.PlatformTypes));

            CreateMap<CommentResource, Comment>().ReverseMap();
            CreateMap<GenreResource, Genre>()
                .ForMember(c => c.ParentGenre, opt => opt.Ignore())
                .ForMember(c => c.ParentGenre, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PlatformTypeResource, PlatformType>().ReverseMap();
        }
    }
}