namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flocid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FlocId", "OrderItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.FlocId", "OrderItemId");
            AddForeignKey("dbo.FlocId", "OrderItemId", "dbo.OrderItem", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlocId", "OrderItemId", "dbo.OrderItem");
            DropIndex("dbo.FlocId", new[] { "OrderItemId" });
            DropColumn("dbo.FlocId", "OrderItemId");
        }
    }
}
