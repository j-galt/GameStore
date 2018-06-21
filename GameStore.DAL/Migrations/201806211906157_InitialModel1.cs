namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlatformTypeGames", "Type", "dbo.PlatformTypes");
            DropIndex("dbo.PlatformTypeGames", new[] { "Type" });
            RenameColumn(table: "dbo.PlatformTypeGames", name: "Type", newName: "TypeId");
            DropPrimaryKey("dbo.PlatformTypes");
            DropPrimaryKey("dbo.PlatformTypeGames");
            AddColumn("dbo.PlatformTypes", "TypeId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PlatformTypes", "Type", c => c.String());
            AlterColumn("dbo.PlatformTypeGames", "TypeId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PlatformTypes", "TypeId");
            AddPrimaryKey("dbo.PlatformTypeGames", new[] { "TypeId", "GameId" });
            CreateIndex("dbo.PlatformTypeGames", "TypeId");
            AddForeignKey("dbo.PlatformTypeGames", "TypeId", "dbo.PlatformTypes", "TypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatformTypeGames", "TypeId", "dbo.PlatformTypes");
            DropIndex("dbo.PlatformTypeGames", new[] { "TypeId" });
            DropPrimaryKey("dbo.PlatformTypeGames");
            DropPrimaryKey("dbo.PlatformTypes");
            AlterColumn("dbo.PlatformTypeGames", "TypeId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PlatformTypes", "Type", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.PlatformTypes", "TypeId");
            AddPrimaryKey("dbo.PlatformTypeGames", new[] { "Type", "GameId" });
            AddPrimaryKey("dbo.PlatformTypes", "Type");
            RenameColumn(table: "dbo.PlatformTypeGames", name: "TypeId", newName: "Type");
            CreateIndex("dbo.PlatformTypeGames", "Type");
            AddForeignKey("dbo.PlatformTypeGames", "Type", "dbo.PlatformTypes", "Type", cascadeDelete: true);
        }
    }
}
