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
                        CommentId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Body = c.String(),
                        AnswerId = c.String(maxLength: 128),
                        Game_GameId = c.String(nullable: false, maxLength: 128),
                        Author_PublisherId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Comments", t => t.AnswerId)
                .ForeignKey("dbo.Games", t => t.Game_GameId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.Author_PublisherId)
                .Index(t => t.AnswerId)
                .Index(t => t.Game_GameId)
                .Index(t => t.Author_PublisherId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.String(nullable: false, maxLength: 128),
                        GameName = c.String(),
                        Description = c.String(),
                        PublisherId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreName = c.String(nullable: false, maxLength: 128),
                        SubGenreName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GenreName)
                .ForeignKey("dbo.Genres", t => t.SubGenreName)
                .Index(t => t.SubGenreName);
            
            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Type);
            
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        GameId = c.String(nullable: false, maxLength: 128),
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
                        GameId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Type, t.GameId })
                .ForeignKey("dbo.PlatformTypes", t => t.Type, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.Type)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Author_PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Games", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.PlatformTypeGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGames", "Type", "dbo.PlatformTypes");
            DropForeignKey("dbo.GameGenres", "GenreName", "dbo.Genres");
            DropForeignKey("dbo.GameGenres", "GameId", "dbo.Games");
            DropForeignKey("dbo.Genres", "SubGenreName", "dbo.Genres");
            DropForeignKey("dbo.Comments", "Game_GameId", "dbo.Games");
            DropForeignKey("dbo.Comments", "AnswerId", "dbo.Comments");
            DropIndex("dbo.PlatformTypeGames", new[] { "GameId" });
            DropIndex("dbo.PlatformTypeGames", new[] { "Type" });
            DropIndex("dbo.GameGenres", new[] { "GenreName" });
            DropIndex("dbo.GameGenres", new[] { "GameId" });
            DropIndex("dbo.Genres", new[] { "SubGenreName" });
            DropIndex("dbo.Games", new[] { "PublisherId" });
            DropIndex("dbo.Comments", new[] { "Author_PublisherId" });
            DropIndex("dbo.Comments", new[] { "Game_GameId" });
            DropIndex("dbo.Comments", new[] { "AnswerId" });
            DropTable("dbo.PlatformTypeGames");
            DropTable("dbo.GameGenres");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Publishers");
            DropTable("dbo.Comments");
        }
    }
}
