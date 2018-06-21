using GameStore.BLL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.EntityConfigurations
{
    public class PlatformTypeConfiguration : EntityTypeConfiguration<PlatformType>
    {
        public PlatformTypeConfiguration()
        {
            HasKey(pt => pt.Type);

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
