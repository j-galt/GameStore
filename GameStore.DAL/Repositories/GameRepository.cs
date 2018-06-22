using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }

        public Game GetGameWithRelatedData(int id)
        {
            return _dbContext.Games
                .Where(g => g.GameId == id)
                .Include(g => g.Comments)
                .Include(g => g.Genres)
                .Include(g => g.PlatformTypes)
                .Include(g => g.Publisher)
                .FirstOrDefault();
        }

        public void UpdateGame(Game game)
        {
            _dbContext.Games.Attach(game);
        }
    }
}
