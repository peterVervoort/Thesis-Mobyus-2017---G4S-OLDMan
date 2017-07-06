namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeys : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserRoleStateChange", newName: "StateChangeUserRole");
            RenameTable(name: "dbo.UserRoleGroupUserRole", newName: "UserRoleUserRoleGroup");
            RenameTable(name: "dbo.LoginSiteUser", newName: "UserLoginSite");
            DropForeignKey("dbo.DeviceStateChange", "RepairReason_Id", "dbo.RepairReason");
            DropIndex("dbo.DeviceStateChange", new[] { "RepairReason_Id" });
            RenameColumn(table: "dbo.DeviceStateChange", name: "RepairStateChange_Id", newName: "RepairStateChangeId");
            RenameColumn(table: "dbo.StateChange", name: "StateFrom_Id", newName: "StateFromId");
            RenameColumn(table: "dbo.StateChange", name: "StateTo_Id", newName: "StateToId");
            RenameColumn(table: "dbo.LoginLicence", name: "LoginSite_Id", newName: "LoginSiteId");
            RenameColumn(table: "dbo.MobileDevice", name: "LoginSite_Id", newName: "LoginSiteId");
            RenameColumn(table: "dbo.MobileDevice", name: "LwpSetting_Id", newName: "LwpSettingId");
            RenameColumn(table: "dbo.DeviceStateChange", name: "MobileDevice_Id", newName: "MobileDeviceId");
            RenameColumn(table: "dbo.ToBeTreatedMobileDevice", name: "LoginSite_Id", newName: "LoginSiteId");
            RenameIndex(table: "dbo.DeviceStateChange", name: "IX_MobileDevice_Id", newName: "IX_MobileDeviceId");
            RenameIndex(table: "dbo.DeviceStateChange", name: "IX_RepairStateChange_Id", newName: "IX_RepairStateChangeId");
            RenameIndex(table: "dbo.MobileDevice", name: "IX_LwpSetting_Id", newName: "IX_LwpSettingId");
            RenameIndex(table: "dbo.MobileDevice", name: "IX_LoginSite_Id", newName: "IX_LoginSiteId");
            RenameIndex(table: "dbo.StateChange", name: "IX_StateFrom_Id", newName: "IX_StateFromId");
            RenameIndex(table: "dbo.StateChange", name: "IX_StateTo_Id", newName: "IX_StateToId");
            RenameIndex(table: "dbo.LoginLicence", name: "IX_LoginSite_Id", newName: "IX_LoginSiteId");
            RenameIndex(table: "dbo.ToBeTreatedMobileDevice", name: "IX_LoginSite_Id", newName: "IX_LoginSiteId");
            DropPrimaryKey("dbo.StateChangeUserRole");
            DropPrimaryKey("dbo.UserRoleUserRoleGroup");
            DropPrimaryKey("dbo.UserLoginSite");
            AddColumn("dbo.DeviceStateChange", "RepairReason", c => c.String());
            AddPrimaryKey("dbo.StateChangeUserRole", new[] { "StateChange_Id", "UserRole_Id" });
            AddPrimaryKey("dbo.UserRoleUserRoleGroup", new[] { "UserRole_Id", "UserRoleGroup_Id" });
            AddPrimaryKey("dbo.UserLoginSite", new[] { "User_Id", "LoginSite_Id" });
            DropColumn("dbo.DeviceStateChange", "RepairReason_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviceStateChange", "RepairReason_Id", c => c.Int());
            DropPrimaryKey("dbo.UserLoginSite");
            DropPrimaryKey("dbo.UserRoleUserRoleGroup");
            DropPrimaryKey("dbo.StateChangeUserRole");
            DropColumn("dbo.DeviceStateChange", "RepairReason");
            AddPrimaryKey("dbo.UserLoginSite", new[] { "LoginSite_Id", "User_Id" });
            AddPrimaryKey("dbo.UserRoleUserRoleGroup", new[] { "UserRoleGroup_Id", "UserRole_Id" });
            AddPrimaryKey("dbo.StateChangeUserRole", new[] { "UserRole_Id", "StateChange_Id" });
            RenameIndex(table: "dbo.ToBeTreatedMobileDevice", name: "IX_LoginSiteId", newName: "IX_LoginSite_Id");
            RenameIndex(table: "dbo.LoginLicence", name: "IX_LoginSiteId", newName: "IX_LoginSite_Id");
            RenameIndex(table: "dbo.StateChange", name: "IX_StateToId", newName: "IX_StateTo_Id");
            RenameIndex(table: "dbo.StateChange", name: "IX_StateFromId", newName: "IX_StateFrom_Id");
            RenameIndex(table: "dbo.MobileDevice", name: "IX_LoginSiteId", newName: "IX_LoginSite_Id");
            RenameIndex(table: "dbo.MobileDevice", name: "IX_LwpSettingId", newName: "IX_LwpSetting_Id");
            RenameIndex(table: "dbo.DeviceStateChange", name: "IX_RepairStateChangeId", newName: "IX_RepairStateChange_Id");
            RenameIndex(table: "dbo.DeviceStateChange", name: "IX_MobileDeviceId", newName: "IX_MobileDevice_Id");
            RenameColumn(table: "dbo.ToBeTreatedMobileDevice", name: "LoginSiteId", newName: "LoginSite_Id");
            RenameColumn(table: "dbo.DeviceStateChange", name: "MobileDeviceId", newName: "MobileDevice_Id");
            RenameColumn(table: "dbo.MobileDevice", name: "LwpSettingId", newName: "LwpSetting_Id");
            RenameColumn(table: "dbo.MobileDevice", name: "LoginSiteId", newName: "LoginSite_Id");
            RenameColumn(table: "dbo.LoginLicence", name: "LoginSiteId", newName: "LoginSite_Id");
            RenameColumn(table: "dbo.StateChange", name: "StateToId", newName: "StateTo_Id");
            RenameColumn(table: "dbo.StateChange", name: "StateFromId", newName: "StateFrom_Id");
            RenameColumn(table: "dbo.DeviceStateChange", name: "RepairStateChangeId", newName: "RepairStateChange_Id");
            CreateIndex("dbo.DeviceStateChange", "RepairReason_Id");
            AddForeignKey("dbo.DeviceStateChange", "RepairReason_Id", "dbo.RepairReason", "Id");
            RenameTable(name: "dbo.UserLoginSite", newName: "LoginSiteUser");
            RenameTable(name: "dbo.UserRoleUserRoleGroup", newName: "UserRoleGroupUserRole");
            RenameTable(name: "dbo.StateChangeUserRole", newName: "UserRoleStateChange");
        }
    }
}
