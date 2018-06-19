using GameStore.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            HasKey(g => g.GenreName);

            Property(g => g.GenreName)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Self relationships.
            HasOptional(g => g.SubGenre)
                .WithMany()
                .HasForeignKey(g => g.SubGenreName);
        }
    }
}
