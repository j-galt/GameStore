using GameStore.BLL.Entities;

namespace GameStore.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(GameStoreDbContext dbContext) : base(dbContext) 
        {
        }
    }
}
