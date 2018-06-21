namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PlatformTypeGames", name: "TypeId", newName: "Type");
            RenameIndex(table: "dbo.PlatformTypeGames", name: "IX_TypeId", newName: "IX_Type");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PlatformTypeGames", name: "IX_Type", newName: "IX_TypeId");
            RenameColumn(table: "dbo.PlatformTypeGames", name: "Type", newName: "TypeId");
        }
    }
}
