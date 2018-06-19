namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialModel : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "AnswerId" });
            DropIndex("dbo.Genres", new[] { "SubGenreName" });
            RenameColumn(table: "dbo.Games", name: "Publisher_PublisherId", newName: "PublisherId");
            RenameIndex(table: "dbo.Games", name: "IX_Publisher_PublisherId", newName: "IX_PublisherId");
            AlterColumn("dbo.Comments", "AnswerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Genres", "SubGenreName", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "AnswerId");
            CreateIndex("dbo.Genres", "SubGenreName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Genres", new[] { "SubGenreName" });
            DropIndex("dbo.Comments", new[] { "AnswerId" });
            AlterColumn("dbo.Genres", "SubGenreName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "AnswerId", c => c.String(nullable: false, maxLength: 128));
            RenameIndex(table: "dbo.Games", name: "IX_PublisherId", newName: "IX_Publisher_PublisherId");
            RenameColumn(table: "dbo.Games", name: "PublisherId", newName: "Publisher_PublisherId");
            CreateIndex("dbo.Genres", "SubGenreName");
            CreateIndex("dbo.Comments", "AnswerId");
        }
    }
}
