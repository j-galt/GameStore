using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GameStore.BLL.Entities;
using GameStore.BLL.Interfaces;

namespace GameStore.DAL.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(GameStoreDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Genre> GetGenresByNames(IEnumerable<string> names)
        {
            return _dbContext.Genres
                .Where(g => names.Contains(g.GenreName));
        }
    }
}
