namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlocId", "LoginLicenceId", "dbo.LoginLicence");
            DropIndex("dbo.FlocId", new[] { "LoginLicenceId" });
            RenameColumn(table: "dbo.FlocId", name: "LoginLicenceId", newName: "LoginLicence_Id");
            AlterColumn("dbo.FlocId", "LoginLicence_Id", c => c.Int());
            CreateIndex("dbo.FlocId", "LoginLicence_Id");
            AddForeignKey("dbo.FlocId", "LoginLicence_Id", "dbo.LoginLicence", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlocId", "LoginLicence_Id", "dbo.LoginLicence");
            DropIndex("dbo.FlocId", new[] { "LoginLicence_Id" });
            AlterColumn("dbo.FlocId", "LoginLicence_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.FlocId", name: "LoginLicence_Id", newName: "LoginLicenceId");
            CreateIndex("dbo.FlocId", "LoginLicenceId");
            AddForeignKey("dbo.FlocId", "LoginLicenceId", "dbo.LoginLicence", "Id", cascadeDelete: true);
        }
    }
}
