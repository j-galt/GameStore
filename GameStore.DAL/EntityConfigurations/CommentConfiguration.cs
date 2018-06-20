using GameStore.BLL.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GameStore.DAL.EntityConfigurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        { 
            Property(g => g.CommentId)                
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Self relationship.
            HasOptional(c => c.ParentComment)
                .WithMany()
                .HasForeignKey(c => c.ParentCommentId);
        }
    }
}
