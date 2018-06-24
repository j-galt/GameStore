using GameStore.BLL.Entities;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService : IService<Game>
    {
        Game Get(int id);
        IEnumerable<Game> GetGamesByGenre(string name);
        IEnumerable<Game> GetGamesByPlatformTypes(IEnumerable<PlatformType> platformTypes);
    }
}
