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
            CreateMap<Game, GetGameResource>();

            // ApiResource to Domain.
            CreateMap<CreateGameResource, Game>()
                .ForMember(g => g.GameId, opt => opt.Ignore());

            CreateMap<CommentResource, Comment>()
                .ForMember(c => c.CommentId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GenreResource, Genre>()
                .ReverseMap();

            CreateMap<PlatformTypeResource, PlatformType>()              
                .ReverseMap();
        }
    }
}