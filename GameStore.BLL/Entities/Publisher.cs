using System.Collections.Generic;

namespace GameStore.BLL.Entities
{
    public class Publisher
    {
        public Publisher()
        {
            Games = new List<Game>();
        }

        public int PublisherId { get; set; }
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
