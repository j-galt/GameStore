using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ApiResources
{
    public class GameResource
    {
        public GameResource()
        {
            Comments = new List<CommentResource>();
        }

        public string GameName { get; set; }
        public string Description { get; set; }
        public int PublisherName { get; set; }

        IEnumerable<CommentResource> Comments { get; set; }
    }
}