namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class systemstatechange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StateChange", "SystemStateChange", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StateChange", "SystemStateChange");
        }
    }
}
