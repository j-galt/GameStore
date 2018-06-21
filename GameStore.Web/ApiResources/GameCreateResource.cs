using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ApiResources
{
    public class GameCreateResource
    {
        public string GameName { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }

        public ICollection<GenreResource> Genres { get; set; }
        public ICollection<PlatformTypeResource> PlatformTypes { get; set; }
    }
}