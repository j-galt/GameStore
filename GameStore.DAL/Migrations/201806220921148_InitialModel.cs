namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Body = c.String(),
                        GameId = c.Int(nullable: false),
                        ParentCommentId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .Index(t => t.GameId)
                .Index(t => t.ParentCommentId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                        Description = c.String(),
                        PublisherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreName = c.String(nullable: false, maxLength: 128),
                        ParentGenreName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GenreName)
                .ForeignKey("dbo.Genres", t => t.ParentGenreName)
                .Index(t => t.ParentGenreName);
            
            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Type);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        GenreName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GameId, t.GenreName })
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreName, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.GenreName);
            
            CreateTable(
                "dbo.PlatformTypeGames",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 128),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Type, t.GameId })
                .ForeignKey("dbo.PlatformTypes", t => t.Type, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.Type)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Games", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.PlatformTypeGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGames", "Type", "dbo.PlatformTypes");
            DropForeignKey("dbo.GameGenres", "GenreName", "dbo.Genres");
            DropForeignKey("dbo.GameGenres", "GameId", "dbo.Games");
            DropForeignKey("dbo.Genres", "ParentGenreName", "dbo.Genres");
            DropForeignKey("dbo.Comments", "GameId", "dbo.Games");
            DropIndex("dbo.PlatformTypeGames", new[] { "GameId" });
            DropIndex("dbo.PlatformTypeGames", new[] { "Type" });
            DropIndex("dbo.GameGenres", new[] { "GenreName" });
            DropIndex("dbo.GameGenres", new[] { "GameId" });
            DropIndex("dbo.Genres", new[] { "ParentGenreName" });
            DropIndex("dbo.Games", new[] { "PublisherId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "GameId" });
            DropTable("dbo.PlatformTypeGames");
            DropTable("dbo.GameGenres");
            DropTable("dbo.Publishers");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
        }
    }
}
