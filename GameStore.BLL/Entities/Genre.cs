using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.BLL.Entities
{
    public class Genre
    {
        public Genre()
        {
            Games = new List<Game>();
        }

        public string GenreName { get; set; }
        public string ParentGenreName { get; set; }

        public ICollection<Game> Games { get; set; }        
        public Genre ParentGenre { get; set; }
    }
}
