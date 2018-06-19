using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.DAL.Entities
{
    public class Genre
    {
        public Genre()
        {
            Games = new List<Game>();
        }

        public string GenreName { get; set; }
        public string SubGenreName { get; set; }

        public ICollection<Game> Games { get; set; }        
        public Genre SubGenre { get; set; }
    }
}
