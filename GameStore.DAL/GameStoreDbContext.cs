using GameStore.DAL.Entities;
using System.Data.Entity;
using GameStore.DAL.EntityConfigurations;

namespace GameStore.DAL
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(string connStr) : base(connStr)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new PlatformTypeConfiguration());
            modelBuilder.Configurations.Add(new PublisherConfiguration());
        }
    }
}
