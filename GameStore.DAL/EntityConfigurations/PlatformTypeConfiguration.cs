using GameStore.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.EntityConfigurations
{
    public class PlatformTypeConfiguration : EntityTypeConfiguration<PlatformType>
    {
        public PlatformTypeConfiguration()
        {
            HasKey(g => g.Type);

            Property(g => g.Type)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Many to many.
            HasMany(pt => pt.Games)
                .WithMany(g => g.PlatformTypes)
                .Map(m =>
                {
                    m.ToTable("PlatformTypeGames");
                    m.MapLeftKey("Type");
                    m.MapRightKey("GameId");
                });
        }
    }
}
