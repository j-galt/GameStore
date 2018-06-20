using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<Game, GameDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<PlatformType, PlatformTypeDto>();
            CreateMap<Publisher, PublisherDto>();
        }
    }
}
