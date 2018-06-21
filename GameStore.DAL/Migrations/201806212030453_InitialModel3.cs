namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlatformTypes", "TypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlatformTypes", "TypeId", c => c.Int(nullable: false));
        }
    }
}
