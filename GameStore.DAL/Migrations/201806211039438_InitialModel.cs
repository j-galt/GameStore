namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Author_PublisherId", "dbo.Publishers");
            DropIndex("dbo.Comments", new[] { "Author_PublisherId" });
            DropIndex("dbo.Games", new[] { "Publisher_PublisherId" });
            DropColumn("dbo.Games", "PublisherId");
            RenameColumn(table: "dbo.Comments", name: "Game_GameId", newName: "GameId");
            RenameColumn(table: "dbo.Games", name: "Publisher_PublisherId", newName: "PublisherId");
            RenameIndex(table: "dbo.Comments", name: "IX_Game_GameId", newName: "IX_GameId");
            AlterColumn("dbo.Games", "PublisherId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "PublisherId");
            DropColumn("dbo.Comments", "Author_PublisherId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Author_PublisherId", c => c.Int());
            DropIndex("dbo.Games", new[] { "PublisherId" });
            AlterColumn("dbo.Games", "PublisherId", c => c.String());
            RenameIndex(table: "dbo.Comments", name: "IX_GameId", newName: "IX_Game_GameId");
            RenameColumn(table: "dbo.Games", name: "PublisherId", newName: "Publisher_PublisherId");
            RenameColumn(table: "dbo.Comments", name: "GameId", newName: "Game_GameId");
            AddColumn("dbo.Games", "PublisherId", c => c.String());
            CreateIndex("dbo.Games", "Publisher_PublisherId");
            CreateIndex("dbo.Comments", "Author_PublisherId");
            AddForeignKey("dbo.Comments", "Author_PublisherId", "dbo.Publishers", "PublisherId");
        }
    }
}
