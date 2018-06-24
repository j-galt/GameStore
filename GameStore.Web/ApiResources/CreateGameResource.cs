using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ApiResources
{
    public class CreateGameResource
    {
        public CreateGameResource()
        {
            Genres = new List<GenreResource>();
            PlatformTypes = new List<PlatformTypeResource>();
        }

        public string GameName { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }

        public IEnumerable<GenreResource> Genres { get; set; }
        public IEnumerable<PlatformTypeResource> PlatformTypes { get; set; }
    }
}