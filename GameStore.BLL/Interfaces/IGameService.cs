using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces
{
    public interface IGameService
    {
        GameDto GetGame(string id);
        IEnumerable<GameDto> GetAllGames();

        void CreateGame(GameDto gameDto);
        GameDto EditGame(string id, GameDto gameDto);

        void DeleteGame(GameDto gameDto);
    }
}
