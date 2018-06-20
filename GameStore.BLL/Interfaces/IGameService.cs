using GameStore.BLL.Entities;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        Game GetGame(int id);
        IEnumerable<Game> GetAllGames();

        void CreateGame(Game game);
        Game EditGame(int id, Game updatedGame);

        void DeleteGame(Game game);
    }
}
