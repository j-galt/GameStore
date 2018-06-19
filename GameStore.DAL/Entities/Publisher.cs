using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
    public class Publisher
    {
        public Publisher()
        {
            Games = new List<Game>();
        }

        public string PublisherId { get; set; }
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
