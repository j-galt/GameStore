using GameStore.BLL.Entities;

namespace GameStore.DAL.Repositories
{
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
