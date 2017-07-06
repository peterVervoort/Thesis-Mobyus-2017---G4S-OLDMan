namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flocid_correct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlocId", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.FlocId", "LoginLicence_Id", "dbo.LoginLicence");
            DropIndex("dbo.FlocId", new[] { "OrderItemId" });
            DropIndex("dbo.FlocId", new[] { "LoginLicence_Id" });
            RenameColumn(table: "dbo.FlocId", name: "LoginLicence_Id", newName: "LoginLicenceId");
            AlterColumn("dbo.FlocId", "LoginLicenceId", c => c.Int(nullable: false));
            CreateIndex("dbo.FlocId", "LoginLicenceId");
            AddForeignKey("dbo.FlocId", "LoginLicenceId", "dbo.LoginLicence", "Id", cascadeDelete: true);
            DropColumn("dbo.FlocId", "OrderItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FlocId", "OrderItemId", c => c.Int(nullable: false));
            DropForeignKey("dbo.FlocId", "LoginLicenceId", "dbo.LoginLicence");
            DropIndex("dbo.FlocId", new[] { "LoginLicenceId" });
            AlterColumn("dbo.FlocId", "LoginLicenceId", c => c.Int());
            RenameColumn(table: "dbo.FlocId", name: "LoginLicenceId", newName: "LoginLicence_Id");
            CreateIndex("dbo.FlocId", "LoginLicence_Id");
            CreateIndex("dbo.FlocId", "OrderItemId");
            AddForeignKey("dbo.FlocId", "LoginLicence_Id", "dbo.LoginLicence", "Id");
            AddForeignKey("dbo.FlocId", "OrderItemId", "dbo.OrderItem", "Id", cascadeDelete: true);
        }
    }
}
