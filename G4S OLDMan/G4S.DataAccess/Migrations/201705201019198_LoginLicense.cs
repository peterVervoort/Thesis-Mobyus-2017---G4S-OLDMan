namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginLicense : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.LoginLicence", name: "OrderItem_Id", newName: "OrderItemId");
            RenameIndex(table: "dbo.LoginLicence", name: "IX_OrderItem_Id", newName: "IX_OrderItemId");
            AddColumn("dbo.LoginLicence", "CertificateCreated", c => c.Boolean(nullable: false));
            DropColumn("dbo.LoginLicence", "CreationOfCertificate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoginLicence", "CreationOfCertificate", c => c.DateTime(nullable: false));
            DropColumn("dbo.LoginLicence", "CertificateCreated");
            RenameIndex(table: "dbo.LoginLicence", name: "IX_OrderItemId", newName: "IX_OrderItem_Id");
            RenameColumn(table: "dbo.LoginLicence", name: "OrderItemId", newName: "OrderItem_Id");
        }
    }
}
