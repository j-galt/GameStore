using GameStore.BLL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.EntityConfigurations
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            HasKey(g => g.GenreName);

            // Self relationships.
            HasOptional(g => g.ParentGenre)
                .WithMany()
                .HasForeignKey(g => g.ParentGenreName);
        }
    }
}
