using GameStore.BLL.Entities;

namespace GameStore.DAL.Repositories
{
    public class GameRepository : Repository<Game>
    {
        public GameRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
