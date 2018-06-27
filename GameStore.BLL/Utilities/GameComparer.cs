using GameStore.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Utilities
{
    public class GameComparer : IEqualityComparer<Game>
    {
        public bool Equals(Game x, Game y)
        {
            if (x.GameId != y.GameId)
                return false;

            return x.Description == y.Description 
                && x.GameName == y.GameName;
        }

        public int GetHashCode(Game obj)
        {
            return 1 ^ obj.GameId;
        }
    }
}
