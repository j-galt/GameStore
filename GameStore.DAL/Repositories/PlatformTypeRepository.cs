﻿using GameStore.DAL.Entities;

namespace GameStore.DAL.Repositories
{
    public class PlatformTypeRepository : Repository<PlatformType>
    {
        public PlatformTypeRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }
    }
}