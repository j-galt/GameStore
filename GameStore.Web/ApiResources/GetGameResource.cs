using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ApiResources
{
    public class GetGameResource
    {
        public GetGameResource()
        {
            Comments = new List<CommentResource>();
            Genres = new List<GenreResource>();
            PlatformTypes = new List<PlatformTypeResource>();
        }

        public string GameName { get; set; }
        public string Description { get; set; }
        public string PublisherName { get; set; }

        public IEnumerable<CommentResource> Comments { get; set; }
        public IEnumerable<GenreResource> Genres { get; set; }
        public IEnumerable<PlatformTypeResource> PlatformTypes { get; set; }
    }
}