using AutoMapper;
using GameStore.BLL.Entities;
using GameStore.Web.ApiResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameResource, Game>().ReverseMap();
            CreateMap<CommentResource, Comment>().ReverseMap();
        }
    }
}