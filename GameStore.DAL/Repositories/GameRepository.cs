using GameStore.DAL.Entities;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : Repository<Game>
    {
        public GameRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
