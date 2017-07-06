namespace G4S.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviceStateHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MobileDeviceId = c.Int(),
                        Comment = c.String(),
                        RepairStateChangeId = c.Int(),
                        ChangedById = c.Int(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ChangedById, cascadeDelete: true)
                .ForeignKey("dbo.MobileDevice", t => t.MobileDeviceId)
                .ForeignKey("dbo.StateChange", t => t.RepairStateChangeId)
                .Index(t => t.MobileDeviceId)
                .Index(t => t.RepairStateChangeId)
                .Index(t => t.ChangedById);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        RoleGroupId = c.Int(),
                        LanguageId = c.Int(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGroup", t => t.RoleGroupId)
                .Index(t => t.RoleGroupId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Taal = c.String(nullable: false),
                        ShortCode = c.String(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Translation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.Int(nullable: false),
                        Group = c.String(),
                        Keyword = c.String(),
                        Value = c.String(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.LoginSite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteName = c.String(),
                        CsvSynonyms = c.String(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoleGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        AutoLinkEveryGroup = c.Boolean(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderStateChange",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateFromId = c.Int(),
                        StateToId = c.Int(),
                        ProductTypeId = c.Int(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductType", t => t.ProductTypeId, cascadeDelete: true)
                .ForeignKey("dbo.State", t => t.StateFromId)
                .ForeignKey("dbo.State", t => t.StateToId)
                .Index(t => t.StateFromId)
                .Index(t => t.StateToId)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        DeviceTypeRequired = c.Boolean(nullable: false),
                        HasOrderStates = c.Boolean(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Tag = c.String(),
                        KindId = c.Int(nullable: false),
                        ColorHex = c.String(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StateKind", t => t.KindId, cascadeDelete: true)
                .Index(t => t.KindId);
            
            CreateTable(
                "dbo.StateKind",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                        SystemRole = c.Boolean(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StateChange",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateFromId = c.Int(),
                        StateToId = c.Int(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        UserRole_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.StateFromId)
                .ForeignKey("dbo.State", t => t.StateToId)
                .ForeignKey("dbo.UserRole", t => t.UserRole_Id)
                .Index(t => t.StateFromId)
                .Index(t => t.StateToId)
                .Index(t => t.UserRole_Id);
            
            CreateTable(
                "dbo.MobileDevice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderItemId = c.Int(),
                        DeviceName = c.String(),
                        Reference = c.String(),
                        DeviceTypeId = c.Int(),
                        PhoneNumber = c.String(),
                        PlatformId = c.Int(),
                        Country = c.String(),
                        LoginSiteId = c.Int(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoginSite", t => t.LoginSiteId)
                .ForeignKey("dbo.OrderItem", t => t.OrderItemId)
                .ForeignKey("dbo.Platform", t => t.PlatformId)
                .ForeignKey("dbo.DeviceType", t => t.DeviceTypeId)
                .Index(t => t.OrderItemId)
                .Index(t => t.DeviceTypeId)
                .Index(t => t.PlatformId)
                .Index(t => t.LoginSiteId);
            
            CreateTable(
                "dbo.LwpSetting",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TelephoneAlarmActivated = c.Boolean(),
                        PhoneNumbersForTelephoneAlarm = c.String(),
                        PanicButtonActivated = c.Boolean(),
                        MovementDetectionActivated = c.Boolean(),
                        TimeBeforeMovementAlarmInSeconds = c.Int(),
                        ManDownAlarmActivated = c.Boolean(),
                        AngleOfManDownDetection = c.Int(),
                        TimeBeforeManDownAlarmInSeconds = c.Int(),
                        SchockAlarmActivated = c.Boolean(),
                        FallAlarmActivated = c.Boolean(),
                        TimerAlarmActivated = c.Boolean(),
                        TimeBeforeTimerAlarmInSeconds = c.Int(),
                        SendAlarmToExternalAlarmReciverActivated = c.Boolean(),
                        UniqueIdentifierToSendToExternalAlarmReciever = c.Int(),
                        ExitGeofenceAreaWhenUserSignsOff = c.Boolean(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MobileDevice", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        CostCenter = c.String(),
                        QuantityOfProducts = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        DeviceTypeId = c.Int(),
                        DeliveryOfSupplier = c.DateTime(),
                        DeliveryToOperations = c.DateTime(),
                        AnnulationDate = c.DateTime(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceType", t => t.DeviceTypeId)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .ForeignKey("dbo.ProductType", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.TypeId)
                .Index(t => t.DeviceTypeId);
            
            CreateTable(
                "dbo.DeviceType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false),
                        LwpSettingPossible = c.Boolean(nullable: false),
                        CsvSynonyms = c.String(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItemHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderItemId = c.Int(),
                        StateChangeId = c.Int(),
                        ChangedById = c.Int(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ChangedById, cascadeDelete: true)
                .ForeignKey("dbo.OrderItem", t => t.OrderItemId)
                .ForeignKey("dbo.OrderStateChange", t => t.StateChangeId)
                .Index(t => t.OrderItemId)
                .Index(t => t.StateChangeId)
                .Index(t => t.ChangedById);
            
            CreateTable(
                "dbo.LoginLicence",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationOfCertificate = c.DateTime(nullable: false),
                        PlatformId = c.Int(),
                        Country = c.String(),
                        LoginSiteId = c.Int(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        OrderItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoginSite", t => t.LoginSiteId)
                .ForeignKey("dbo.Platform", t => t.PlatformId)
                .ForeignKey("dbo.OrderItem", t => t.OrderItem_Id)
                .Index(t => t.PlatformId)
                .Index(t => t.LoginSiteId)
                .Index(t => t.OrderItem_Id);
            
            CreateTable(
                "dbo.FlocId",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginSiteId = c.Int(nullable: false),
                        FlocIdNumber = c.Int(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        LoginLicence_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoginSite", t => t.LoginSiteId, cascadeDelete: true)
                .ForeignKey("dbo.LoginLicence", t => t.LoginLicence_Id)
                .Index(t => t.LoginSiteId)
                .Index(t => t.LoginLicence_Id);
            
            CreateTable(
                "dbo.Platform",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlatformName = c.String(),
                        CsvSynonyms = c.String(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderNumber = c.Long(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        AnnulationDate = c.DateTime(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RepairReason",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reason = c.String(nullable: false),
                        StateId = c.Int(nullable: false),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.ToBeTreatedLwpSetting",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TelephoneAlarmActivated = c.Boolean(),
                        PhoneNumbersForTelephoneAlarm = c.String(),
                        PanicButtonActivated = c.Boolean(),
                        MovementDetectionActivated = c.Boolean(),
                        TimeBeforeMovementAlarmInSeconds = c.Int(),
                        ManDownAlarmActivated = c.Boolean(),
                        AngleOfManDownDetection = c.Int(),
                        TimeBeforeManDownAlarmInSeconds = c.Int(),
                        SchockAlarmActivated = c.Boolean(),
                        FallAlarmActivated = c.Boolean(),
                        TimerAlarmActivated = c.Boolean(),
                        TimeBeforeTimerAlarmInSeconds = c.Int(),
                        SendAlarmToExternalAlarmReciverActivated = c.Boolean(),
                        UniqueIdentifierToSendToExternalAlarmReciever = c.Int(),
                        ExitGeofenceAreaWhenUserSignsOff = c.Boolean(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToBeTreatedMobileDevice", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ToBeTreatedMobileDevice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceName = c.String(),
                        Reference = c.String(),
                        DeviceTypeId = c.Int(),
                        DeviceTypeOriginal = c.String(),
                        LoginSiteOriginal = c.String(),
                        PlatformOriginal = c.String(),
                        PhoneNumber = c.String(),
                        PlatformId = c.Int(),
                        Country = c.String(),
                        LoginSiteId = c.Int(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoginSite", t => t.LoginSiteId)
                .ForeignKey("dbo.Platform", t => t.PlatformId)
                .ForeignKey("dbo.DeviceType", t => t.DeviceTypeId)
                .Index(t => t.DeviceTypeId)
                .Index(t => t.PlatformId)
                .Index(t => t.LoginSiteId);
            
            CreateTable(
                "dbo.ValidationWarning",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Warning = c.String(),
                        SoftDelete = c.Boolean(nullable: false),
                        DeletedAtUtc = c.DateTimeOffset(precision: 7),
                        CreatedAtUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        ToBeTreatedMobileDevice_Id = c.Int(),
                        ToBeTreatedLwpSetting_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToBeTreatedMobileDevice", t => t.ToBeTreatedMobileDevice_Id)
                .ForeignKey("dbo.ToBeTreatedLwpSetting", t => t.ToBeTreatedLwpSetting_Id)
                .Index(t => t.ToBeTreatedMobileDevice_Id)
                .Index(t => t.ToBeTreatedLwpSetting_Id);
            
            CreateTable(
                "dbo.LoginSiteUser",
                c => new
                    {
                        LoginSite_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginSite_Id, t.User_Id })
                .ForeignKey("dbo.LoginSite", t => t.LoginSite_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.LoginSite_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.OrderStateChangeUserRoleGroup",
                c => new
                    {
                        OrderStateChange_Id = c.Int(nullable: false),
                        UserRoleGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderStateChange_Id, t.UserRoleGroup_Id })
                .ForeignKey("dbo.OrderStateChange", t => t.OrderStateChange_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGroup", t => t.UserRoleGroup_Id, cascadeDelete: true)
                .Index(t => t.OrderStateChange_Id)
                .Index(t => t.UserRoleGroup_Id);
            
            CreateTable(
                "dbo.UserRoleUserRoleGroup",
                c => new
                    {
                        UserRole_Id = c.Int(nullable: false),
                        UserRoleGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_Id, t.UserRoleGroup_Id })
                .ForeignKey("dbo.UserRole", t => t.UserRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGroup", t => t.UserRoleGroup_Id, cascadeDelete: true)
                .Index(t => t.UserRole_Id)
                .Index(t => t.UserRoleGroup_Id);
            
            CreateTable(
                "dbo.StateChangeUserRoleGroup",
                c => new
                    {
                        StateChange_Id = c.Int(nullable: false),
                        UserRoleGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StateChange_Id, t.UserRoleGroup_Id })
                .ForeignKey("dbo.StateChange", t => t.StateChange_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGroup", t => t.UserRoleGroup_Id, cascadeDelete: true)
                .Index(t => t.StateChange_Id)
                .Index(t => t.UserRoleGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValidationWarning", "ToBeTreatedLwpSetting_Id", "dbo.ToBeTreatedLwpSetting");
            DropForeignKey("dbo.ValidationWarning", "ToBeTreatedMobileDevice_Id", "dbo.ToBeTreatedMobileDevice");
            DropForeignKey("dbo.ToBeTreatedMobileDevice", "DeviceTypeId", "dbo.DeviceType");
            DropForeignKey("dbo.ToBeTreatedMobileDevice", "PlatformId", "dbo.Platform");
            DropForeignKey("dbo.ToBeTreatedLwpSetting", "Id", "dbo.ToBeTreatedMobileDevice");
            DropForeignKey("dbo.ToBeTreatedMobileDevice", "LoginSiteId", "dbo.LoginSite");
            DropForeignKey("dbo.RepairReason", "StateId", "dbo.State");
            DropForeignKey("dbo.DeviceStateHistory", "RepairStateChangeId", "dbo.StateChange");
            DropForeignKey("dbo.MobileDevice", "DeviceTypeId", "dbo.DeviceType");
            DropForeignKey("dbo.DeviceStateHistory", "MobileDeviceId", "dbo.MobileDevice");
            DropForeignKey("dbo.MobileDevice", "PlatformId", "dbo.Platform");
            DropForeignKey("dbo.OrderItem", "TypeId", "dbo.ProductType");
            DropForeignKey("dbo.OrderItem", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.MobileDevice", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.LoginLicence", "OrderItem_Id", "dbo.OrderItem");
            DropForeignKey("dbo.LoginLicence", "PlatformId", "dbo.Platform");
            DropForeignKey("dbo.LoginLicence", "LoginSiteId", "dbo.LoginSite");
            DropForeignKey("dbo.FlocId", "LoginLicence_Id", "dbo.LoginLicence");
            DropForeignKey("dbo.FlocId", "LoginSiteId", "dbo.LoginSite");
            DropForeignKey("dbo.OrderItemHistory", "StateChangeId", "dbo.OrderStateChange");
            DropForeignKey("dbo.OrderItemHistory", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.OrderItemHistory", "ChangedById", "dbo.User");
            DropForeignKey("dbo.OrderItem", "DeviceTypeId", "dbo.DeviceType");
            DropForeignKey("dbo.LwpSetting", "Id", "dbo.MobileDevice");
            DropForeignKey("dbo.MobileDevice", "LoginSiteId", "dbo.LoginSite");
            DropForeignKey("dbo.DeviceStateHistory", "ChangedById", "dbo.User");
            DropForeignKey("dbo.User", "RoleGroupId", "dbo.UserRoleGroup");
            DropForeignKey("dbo.StateChange", "UserRole_Id", "dbo.UserRole");
            DropForeignKey("dbo.StateChange", "StateToId", "dbo.State");
            DropForeignKey("dbo.StateChange", "StateFromId", "dbo.State");
            DropForeignKey("dbo.StateChangeUserRoleGroup", "UserRoleGroup_Id", "dbo.UserRoleGroup");
            DropForeignKey("dbo.StateChangeUserRoleGroup", "StateChange_Id", "dbo.StateChange");
            DropForeignKey("dbo.UserRoleUserRoleGroup", "UserRoleGroup_Id", "dbo.UserRoleGroup");
            DropForeignKey("dbo.UserRoleUserRoleGroup", "UserRole_Id", "dbo.UserRole");
            DropForeignKey("dbo.OrderStateChange", "StateToId", "dbo.State");
            DropForeignKey("dbo.OrderStateChange", "StateFromId", "dbo.State");
            DropForeignKey("dbo.State", "KindId", "dbo.StateKind");
            DropForeignKey("dbo.OrderStateChange", "ProductTypeId", "dbo.ProductType");
            DropForeignKey("dbo.OrderStateChangeUserRoleGroup", "UserRoleGroup_Id", "dbo.UserRoleGroup");
            DropForeignKey("dbo.OrderStateChangeUserRoleGroup", "OrderStateChange_Id", "dbo.OrderStateChange");
            DropForeignKey("dbo.LoginSiteUser", "User_Id", "dbo.User");
            DropForeignKey("dbo.LoginSiteUser", "LoginSite_Id", "dbo.LoginSite");
            DropForeignKey("dbo.User", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Translation", "LanguageId", "dbo.Language");
            DropIndex("dbo.StateChangeUserRoleGroup", new[] { "UserRoleGroup_Id" });
            DropIndex("dbo.StateChangeUserRoleGroup", new[] { "StateChange_Id" });
            DropIndex("dbo.UserRoleUserRoleGroup", new[] { "UserRoleGroup_Id" });
            DropIndex("dbo.UserRoleUserRoleGroup", new[] { "UserRole_Id" });
            DropIndex("dbo.OrderStateChangeUserRoleGroup", new[] { "UserRoleGroup_Id" });
            DropIndex("dbo.OrderStateChangeUserRoleGroup", new[] { "OrderStateChange_Id" });
            DropIndex("dbo.LoginSiteUser", new[] { "User_Id" });
            DropIndex("dbo.LoginSiteUser", new[] { "LoginSite_Id" });
            DropIndex("dbo.ValidationWarning", new[] { "ToBeTreatedLwpSetting_Id" });
            DropIndex("dbo.ValidationWarning", new[] { "ToBeTreatedMobileDevice_Id" });
            DropIndex("dbo.ToBeTreatedMobileDevice", new[] { "LoginSiteId" });
            DropIndex("dbo.ToBeTreatedMobileDevice", new[] { "PlatformId" });
            DropIndex("dbo.ToBeTreatedMobileDevice", new[] { "DeviceTypeId" });
            DropIndex("dbo.ToBeTreatedLwpSetting", new[] { "Id" });
            DropIndex("dbo.RepairReason", new[] { "StateId" });
            DropIndex("dbo.FlocId", new[] { "LoginLicence_Id" });
            DropIndex("dbo.FlocId", new[] { "LoginSiteId" });
            DropIndex("dbo.LoginLicence", new[] { "OrderItem_Id" });
            DropIndex("dbo.LoginLicence", new[] { "LoginSiteId" });
            DropIndex("dbo.LoginLicence", new[] { "PlatformId" });
            DropIndex("dbo.OrderItemHistory", new[] { "ChangedById" });
            DropIndex("dbo.OrderItemHistory", new[] { "StateChangeId" });
            DropIndex("dbo.OrderItemHistory", new[] { "OrderItemId" });
            DropIndex("dbo.OrderItem", new[] { "DeviceTypeId" });
            DropIndex("dbo.OrderItem", new[] { "TypeId" });
            DropIndex("dbo.OrderItem", new[] { "PurchaseOrderId" });
            DropIndex("dbo.LwpSetting", new[] { "Id" });
            DropIndex("dbo.MobileDevice", new[] { "LoginSiteId" });
            DropIndex("dbo.MobileDevice", new[] { "PlatformId" });
            DropIndex("dbo.MobileDevice", new[] { "DeviceTypeId" });
            DropIndex("dbo.MobileDevice", new[] { "OrderItemId" });
            DropIndex("dbo.StateChange", new[] { "UserRole_Id" });
            DropIndex("dbo.StateChange", new[] { "StateToId" });
            DropIndex("dbo.StateChange", new[] { "StateFromId" });
            DropIndex("dbo.State", new[] { "KindId" });
            DropIndex("dbo.OrderStateChange", new[] { "ProductTypeId" });
            DropIndex("dbo.OrderStateChange", new[] { "StateToId" });
            DropIndex("dbo.OrderStateChange", new[] { "StateFromId" });
            DropIndex("dbo.Translation", new[] { "LanguageId" });
            DropIndex("dbo.User", new[] { "LanguageId" });
            DropIndex("dbo.User", new[] { "RoleGroupId" });
            DropIndex("dbo.DeviceStateHistory", new[] { "ChangedById" });
            DropIndex("dbo.DeviceStateHistory", new[] { "RepairStateChangeId" });
            DropIndex("dbo.DeviceStateHistory", new[] { "MobileDeviceId" });
            DropTable("dbo.StateChangeUserRoleGroup");
            DropTable("dbo.UserRoleUserRoleGroup");
            DropTable("dbo.OrderStateChangeUserRoleGroup");
            DropTable("dbo.LoginSiteUser");
            DropTable("dbo.ValidationWarning");
            DropTable("dbo.ToBeTreatedMobileDevice");
            DropTable("dbo.ToBeTreatedLwpSetting");
            DropTable("dbo.RepairReason");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.Platform");
            DropTable("dbo.FlocId");
            DropTable("dbo.LoginLicence");
            DropTable("dbo.OrderItemHistory");
            DropTable("dbo.DeviceType");
            DropTable("dbo.OrderItem");
            DropTable("dbo.LwpSetting");
            DropTable("dbo.MobileDevice");
            DropTable("dbo.StateChange");
            DropTable("dbo.UserRole");
            DropTable("dbo.StateKind");
            DropTable("dbo.State");
            DropTable("dbo.ProductType");
            DropTable("dbo.OrderStateChange");
            DropTable("dbo.UserRoleGroup");
            DropTable("dbo.LoginSite");
            DropTable("dbo.Translation");
            DropTable("dbo.Language");
            DropTable("dbo.User");
            DropTable("dbo.DeviceStateHistory");
        }
    }
}
