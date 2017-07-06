namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isspare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.State", "IsSpare", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.State", "IsSpare");
        }
    }
}
