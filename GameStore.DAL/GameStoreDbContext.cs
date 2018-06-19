using GameStore.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext() : base("GameStoreDbContext")
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .Property(g => g.GameId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Genre>()
                .Property(g => g.GenreName)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Publisher>()
                .Property(g => g.PublisherId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<PlatformType>()
                .Property(g => g.Type)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Comment>()
                .Property(g => g.CommentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Genre>()
                .HasKey(g => g.GenreName);

            modelBuilder.Entity<PlatformType>()
                .HasKey(g => g.Type);

            modelBuilder.Entity<Publisher>()
                .HasMany(p => p.Games)
                .WithRequired(g => g.Publisher);

            modelBuilder.Entity<Game>()
                .HasMany(p => p.Comments)
                .WithRequired(g => g.Game);

            // Many to many.
            modelBuilder.Entity<PlatformType>()
                .HasMany(pt => pt.Games)
                .WithMany(g => g.PlatformTypes)
                .Map(m =>
                {
                    m.ToTable("PlatformTypeGames");
                    m.MapLeftKey("Type");
                    m.MapRightKey("GameId");
                });

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Genres)
                .WithMany(g => g.Games)
                .Map(m =>
                {
                    m.ToTable("GameGenres");
                    m.MapLeftKey("GameId");
                    m.MapRightKey("GenreName");
                });              

            // Self relationships.
            modelBuilder.Entity<Genre>()
                .HasRequired(g => g.SubGenre)
                .WithMany()
                .HasForeignKey(g => g.SubGenreName);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Answer)
                .WithMany()
                .HasForeignKey(c => c.AnswerId);
        }
    }
}
