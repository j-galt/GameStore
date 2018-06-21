namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Genres", "ParentGenreName", "dbo.Genres");
            DropForeignKey("dbo.GameGenres", "GenreName", "dbo.Genres");
            DropForeignKey("dbo.PlatformTypeGames", "TypeId", "dbo.PlatformTypes");
            DropIndex("dbo.PlatformTypeGames", new[] { "TypeId" });
            DropPrimaryKey("dbo.Genres");
            DropPrimaryKey("dbo.PlatformTypes");
            DropPrimaryKey("dbo.PlatformTypeGames");
            AlterColumn("dbo.Genres", "GenreName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PlatformTypes", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.PlatformTypes", "Type", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PlatformTypeGames", "TypeId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Genres", "GenreName");
            AddPrimaryKey("dbo.PlatformTypes", "Type");
            AddPrimaryKey("dbo.PlatformTypeGames", new[] { "TypeId", "GameId" });
            CreateIndex("dbo.PlatformTypeGames", "TypeId");
            AddForeignKey("dbo.Genres", "ParentGenreName", "dbo.Genres", "GenreName");
            AddForeignKey("dbo.GameGenres", "GenreName", "dbo.Genres", "GenreName", cascadeDelete: true);
            AddForeignKey("dbo.PlatformTypeGames", "TypeId", "dbo.PlatformTypes", "Type", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatformTypeGames", "TypeId", "dbo.PlatformTypes");
            DropForeignKey("dbo.GameGenres", "GenreName", "dbo.Genres");
            DropForeignKey("dbo.Genres", "ParentGenreName", "dbo.Genres");
            DropIndex("dbo.PlatformTypeGames", new[] { "TypeId" });
            DropPrimaryKey("dbo.PlatformTypeGames");
            DropPrimaryKey("dbo.PlatformTypes");
            DropPrimaryKey("dbo.Genres");
            AlterColumn("dbo.PlatformTypeGames", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.PlatformTypes", "Type", c => c.String());
            AlterColumn("dbo.PlatformTypes", "TypeId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Genres", "GenreName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PlatformTypeGames", new[] { "TypeId", "GameId" });
            AddPrimaryKey("dbo.PlatformTypes", "TypeId");
            AddPrimaryKey("dbo.Genres", "GenreName");
            CreateIndex("dbo.PlatformTypeGames", "TypeId");
            AddForeignKey("dbo.PlatformTypeGames", "TypeId", "dbo.PlatformTypes", "TypeId", cascadeDelete: true);
            AddForeignKey("dbo.GameGenres", "GenreName", "dbo.Genres", "GenreName", cascadeDelete: true);
            AddForeignKey("dbo.Genres", "ParentGenreName", "dbo.Genres", "GenreName");
        }
    }
}
