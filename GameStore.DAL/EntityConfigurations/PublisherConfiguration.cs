using GameStore.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.EntityConfigurations
{
    public class PublisherConfiguration : EntityTypeConfiguration<Publisher>
    {
        public PublisherConfiguration()
        {
            Property(g => g.PublisherId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasMany(p => p.Games)
                .WithRequired(g => g.Publisher);
        }
    }
}
