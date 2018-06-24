using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class PlatformTypeRepository : Repository<PlatformType>, IPlatformTypeRepository
    {
        public PlatformTypeRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<PlatformType> GetPlatformTypesByNames(IEnumerable<string> names)
        {
            return _dbContext.PlatformTypes
                .Where(pt => names.Contains(pt.Type));
        }
    }
}
