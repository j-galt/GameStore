using GameStore.BLL.Entities;

namespace GameStore.DAL.Repositories
{
    public class PlatformTypeRepository : Repository<PlatformType>
    {
        public PlatformTypeRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}
