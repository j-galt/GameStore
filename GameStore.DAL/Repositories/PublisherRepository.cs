using GameStore.BLL.Entities;

namespace GameStore.DAL.Repositories
{
    public class PublisherRepository : Repository<Publisher>
    {
        public PublisherRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
