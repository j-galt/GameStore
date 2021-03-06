﻿using System.Collections.Generic;

namespace GameStore.BLL.Entities
{
    public class Game
    {
        public Game()
        {
            Genres = new List<Genre>();
            Comments = new List<Comment>();
            PlatformTypes = new List<PlatformType>();
        }

        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PlatformType> PlatformTypes { get; set; }
    }
}
