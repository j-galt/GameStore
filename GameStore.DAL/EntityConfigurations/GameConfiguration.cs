using GameStore.BLL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.EntityConfigurations
{
    public class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {
            Property(g => g.GameId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(p => p.Comments)
                .WithRequired(g => g.Game);

            // Many to many.
            HasMany(g => g.Genres)
                .WithMany(g => g.Games)
                .Map(m =>
                {
                    m.ToTable("GameGenres");
                    m.MapLeftKey("GameId");
                    m.MapRightKey("GenreName");
                });
        }
    }
}
