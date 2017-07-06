namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginLicense_required : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductType", "LoginLicenceRequired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductType", "LoginLicenceRequired");
        }
    }
}
