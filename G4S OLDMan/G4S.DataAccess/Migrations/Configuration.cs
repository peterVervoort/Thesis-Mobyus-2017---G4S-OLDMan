namespace G4S.DataAccess.Migrations
{
    using Entities.Enums;
    using Entities.Pocos;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<G4S.DataAccess.EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(G4S.DataAccess.EntityContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{

            //    System.Diagnostics.Debugger.Launch();

            //}
            var nederlands = new Language { Taal = "Nederlands", ShortCode = "nl" };
            var rgAdmin = new UserRoleGroup { Name = "Admin" };

            var admin = new User { Email = "admin@g4s.be", FirstName = "Admin", Name = "User", Language = nederlands, RoleGroup = rgAdmin };


            context.Languages.AddOrUpdate(l => l.ShortCode,
                nederlands,
                new Language { Taal = "Frans", ShortCode = "fr" },
                new Language { Taal = "Engels", ShortCode = "en" }
            );


            #region UserRoles
            var PoOverview = new UserRole { RoleName = SystemUserRole.PoOverview, SystemRole = true };
            var PoNew = new UserRole { RoleName = SystemUserRole.PoNew, SystemRole = true };
            var PoEdit = new UserRole { RoleName = SystemUserRole.PoEdit, SystemRole = true };
            var PoDelete = new UserRole { RoleName = SystemUserRole.PoDelete, SystemRole = true };
            var PoCSV = new UserRole { RoleName = SystemUserRole.PoCSV, SystemRole = true };
            var PoViewItemLineDetail = new UserRole { RoleName = SystemUserRole.PoViewItemLineDetail, SystemRole = true };
            var ItemLineOverview = new UserRole { RoleName = SystemUserRole.ItemLineOverview, SystemRole = true };
            var ItemLineNew = new UserRole { RoleName = SystemUserRole.ItemLineNew, SystemRole = true };
            var ItemLineEditItemLine = new UserRole { RoleName = SystemUserRole.ItemLineEditItemLine, SystemRole = true };
            var ItemLineDelete = new UserRole { RoleName = SystemUserRole.ItemLineDelete, SystemRole = true };
            var ItemLineLinkDevice = new UserRole { RoleName = SystemUserRole.ItemLineLinkDevice, SystemRole = true };
            var ItemLineCSV = new UserRole { RoleName = SystemUserRole.ItemLineCSV, SystemRole = true };
            var DeviceOverview = new UserRole { RoleName = SystemUserRole.DeviceOverview, SystemRole = true };
            var DeviceNew = new UserRole { RoleName = SystemUserRole.DeviceNew, SystemRole = true };
            var DeviceEdit = new UserRole { RoleName = SystemUserRole.DeviceEdit, SystemRole = true };
            var DeviceDelete = new UserRole { RoleName = SystemUserRole.DeviceDelete, SystemRole = true };
            var DeviceCSV = new UserRole { RoleName = SystemUserRole.DeviceCSV, SystemRole = true };
            var DeviceLinkItemLine = new UserRole { RoleName = SystemUserRole.DeviceLinkItemLine, SystemRole = true };
            var DasBoardX = new UserRole { RoleName = SystemUserRole.DasBoardX, SystemRole = true };
            var Usersverview = new UserRole { RoleName = SystemUserRole.Usersverview, SystemRole = true };
            var UsersNew = new UserRole { RoleName = SystemUserRole.UsersNew, SystemRole = true };
            var UsersEdit = new UserRole { RoleName = SystemUserRole.UsersEdit, SystemRole = true };
            var UsersEditPasswordOnly = new UserRole { RoleName = SystemUserRole.UsersEditPasswordOnly, SystemRole = true };
            var UsersDelete = new UserRole { RoleName = SystemUserRole.UsersDelete, SystemRole = true };
            var UserAddLoginSite = new UserRole { RoleName = SystemUserRole.UserAddLoginSite, SystemRole = true };
            var UsersCSV = new UserRole { RoleName = SystemUserRole.UsersCSV, SystemRole = true };
            var UserRolesView = new UserRole { RoleName = SystemUserRole.UserRolesView, SystemRole = true };
            var UserRolesCSV = new UserRole { RoleName = SystemUserRole.UserRolesCSV, SystemRole = true };
            var UserRolesGroupOverview = new UserRole { RoleName = SystemUserRole.UserRolesGroupOverview, SystemRole = true };
            var UserRoleGroupNew = new UserRole { RoleName = SystemUserRole.UserRoleGroupNew, SystemRole = true };
            var UserRoleGroupEdit = new UserRole { RoleName = SystemUserRole.UserRoleGroupEdit, SystemRole = true };
            var UserRoleGroupDelete = new UserRole { RoleName = SystemUserRole.UserRoleGroupDelete, SystemRole = true };
            var UserRoleGroupAddRole = new UserRole { RoleName = SystemUserRole.UserRoleGroupAddRole, SystemRole = true };
            var UserRoleGroupCSV = new UserRole { RoleName = SystemUserRole.UserRoleGroupCSV, SystemRole = true };
            var LanguageOverview = new UserRole { RoleName = SystemUserRole.LanguageOverview, SystemRole = true };
            var LanguageNew = new UserRole { RoleName = SystemUserRole.LanguageNew, SystemRole = true };
            var LanguageEdit = new UserRole { RoleName = SystemUserRole.LanguageEdit, SystemRole = true };
            var LanguageDelete = new UserRole { RoleName = SystemUserRole.LanguageDelete, SystemRole = true };
            var LanguageCSV = new UserRole { RoleName = SystemUserRole.LanguageCSV, SystemRole = true };
            var TranslationOverview = new UserRole { RoleName = SystemUserRole.TranslationOverview, SystemRole = true };
            var TranslationNew = new UserRole { RoleName = SystemUserRole.TranslationNew, SystemRole = true };
            var TranslationEdit = new UserRole { RoleName = SystemUserRole.TranslationEdit, SystemRole = true };
            var TranslationDelete = new UserRole { RoleName = SystemUserRole.TranslationDelete, SystemRole = true };
            var TranslationToggleMode = new UserRole { RoleName = SystemUserRole.TranslationToggleMode, SystemRole = true };
            var TranslationCSV = new UserRole { RoleName = SystemUserRole.TranslationCSV, SystemRole = true };
            var TranslationAllLanguages = new UserRole { RoleName = SystemUserRole.TranslationAllLanguages, SystemRole = true };
            var StatesOverview = new UserRole { RoleName = SystemUserRole.StatesOverview, SystemRole = true };
            var StatesNew = new UserRole { RoleName = SystemUserRole.StatesNew, SystemRole = true };
            var StatesEdit = new UserRole { RoleName = SystemUserRole.StatesEdit, SystemRole = true };
            var StatesDelete = new UserRole { RoleName = SystemUserRole.StatesDelete, SystemRole = true };
            var StatesCSV = new UserRole { RoleName = SystemUserRole.StatesCSV, SystemRole = true };
            var DeviceStatesFlowOverview = new UserRole { RoleName = SystemUserRole.DeviceStatesFlowOverview, SystemRole = true };
            var DeviceStatesFlowNew = new UserRole { RoleName = SystemUserRole.DeviceStatesFlowNew, SystemRole = true };
            var DeviceStatesFlowEdit = new UserRole { RoleName = SystemUserRole.DeviceStatesFlowEdit, SystemRole = true };
            var DeviceStatesFlowEditRemoveRoleGroups = new UserRole { RoleName = SystemUserRole.DeviceStatesFlowEditRemoveRoleGroups, SystemRole = true };
            var DeviceStatesFlowEditAddRoleGroups = new UserRole { RoleName = SystemUserRole.DeviceStatesFlowEditAddRoleGroups, SystemRole = true };
            var DeviceStatesFlowDelete = new UserRole { RoleName = SystemUserRole.DeviceStatesFlowDelete, SystemRole = true };
            var DeviceStatesFlowCSV = new UserRole { RoleName = SystemUserRole.DeviceStatesFlowCSV, SystemRole = true };
            var OrderStatesFlowOverview = new UserRole { RoleName = SystemUserRole.OrderStatesFlowOverview, SystemRole = true };
            var OrderStatesFlowNew = new UserRole { RoleName = SystemUserRole.OrderStatesFlowNew, SystemRole = true };
            var OrderStatesFlowEdit = new UserRole { RoleName = SystemUserRole.OrderStatesFlowEdit, SystemRole = true };
            var OrderStatesFlowEditRemoveRoleGroups = new UserRole { RoleName = SystemUserRole.OrderStatesFlowEditRemoveRoleGroups, SystemRole = true };
            var OrderStatesFlowEditAddRoleGroups = new UserRole { RoleName = SystemUserRole.OrderStatesFlowEditAddRoleGroups, SystemRole = true };
            var OrderStatesFlowDelete = new UserRole { RoleName = SystemUserRole.OrderStatesFlowDelete, SystemRole = true };
            var OrderStatesFlowCSV = new UserRole { RoleName = SystemUserRole.OrderStatesFlowCSV, SystemRole = true };
            var LoginSiteOverview = new UserRole { RoleName = SystemUserRole.LoginSiteOverview, SystemRole = true };
            var LoginSiteNew = new UserRole { RoleName = SystemUserRole.LoginSiteNew, SystemRole = true };
            var LoginSiteEdit = new UserRole { RoleName = SystemUserRole.LoginSiteEdit, SystemRole = true };
            var LoginSiteDelete = new UserRole { RoleName = SystemUserRole.LoginSiteDelete, SystemRole = true };
            var LoginSiteCSV = new UserRole { RoleName = SystemUserRole.LoginSiteCSV, SystemRole = true };
            var LoginLicenceOverview = new UserRole { RoleName = SystemUserRole.LoginLicenceOverview, SystemRole = true };
            var LoginLicenceNew = new UserRole { RoleName = SystemUserRole.LoginLicenceNew, SystemRole = true };
            var LoginLicenceEdit = new UserRole { RoleName = SystemUserRole.LoginLicenceEdit, SystemRole = true };
            var LoginLicenceDelete = new UserRole { RoleName = SystemUserRole.LoginLicenceDelete, SystemRole = true };
            var LoginLicenceCSV = new UserRole { RoleName = SystemUserRole.LoginLicenceCSV, SystemRole = true };
            var RepairReasonOverview = new UserRole { RoleName = SystemUserRole.RepairReasonOverview, SystemRole = true };
            var RepairReasonSiteNew = new UserRole { RoleName = SystemUserRole.RepairReasonSiteNew, SystemRole = true };
            var RepairReasonEdit = new UserRole { RoleName = SystemUserRole.RepairReasonEdit, SystemRole = true };
            var RepairReasonDelete = new UserRole { RoleName = SystemUserRole.RepairReasonDelete, SystemRole = true };
            var RepairReasonCSV = new UserRole { RoleName = SystemUserRole.RepairReasonCSV, SystemRole = true };
            var PlatformOverview = new UserRole { RoleName = SystemUserRole.PlatformOverview, SystemRole = true };
            var PlatformSiteNew = new UserRole { RoleName = SystemUserRole.PlatformSiteNew, SystemRole = true };
            var PlatformEdit = new UserRole { RoleName = SystemUserRole.PlatformEdit, SystemRole = true };
            var PlatformDelete = new UserRole { RoleName = SystemUserRole.PlatformDelete, SystemRole = true };
            var PlatformCSV = new UserRole { RoleName = SystemUserRole.PlatformCSV, SystemRole = true };
            var FlocIdOverview = new UserRole { RoleName = SystemUserRole.FlocIdOverview, SystemRole = true };
            var FlocIdSiteNew = new UserRole { RoleName = SystemUserRole.FlocIdSiteNew, SystemRole = true };
            var FlocIdEdit = new UserRole { RoleName = SystemUserRole.FlocIdEdit, SystemRole = true };
            var FlocIdDelete = new UserRole { RoleName = SystemUserRole.FlocIdDelete, SystemRole = true };
            var FlocIdCSV = new UserRole { RoleName = SystemUserRole.FlocIdCSV, SystemRole = true };
            var DeviceTypeOverview = new UserRole { RoleName = SystemUserRole.DeviceTypeOverview, SystemRole = true };
            var DeviceTypeSiteNew = new UserRole { RoleName = SystemUserRole.DeviceTypeSiteNew, SystemRole = true };
            var DeviceTypeEdit = new UserRole { RoleName = SystemUserRole.DeviceTypeEdit, SystemRole = true };
            var DeviceTypeDelete = new UserRole { RoleName = SystemUserRole.DeviceTypeDelete, SystemRole = true };
            var DeviceTypeCSV = new UserRole { RoleName = SystemUserRole.DeviceTypeCSV, SystemRole = true };
            var ProductTypeOverview = new UserRole { RoleName = SystemUserRole.ProductTypeOverview, SystemRole = true };
            var ProductTypeSiteNew = new UserRole { RoleName = SystemUserRole.ProductTypeSiteNew, SystemRole = true };
            var ProductTypeEdit = new UserRole { RoleName = SystemUserRole.ProductTypeEdit, SystemRole = true };
            var ProductTypeDelete = new UserRole { RoleName = SystemUserRole.ProductTypeDelete, SystemRole = true };
            var ProductTypeCSV = new UserRole { RoleName = SystemUserRole.ProductTypeCSV, SystemRole = true };
            var CSVImport = new UserRole { RoleName = SystemUserRole.CSVImport, SystemRole = true };
            var OrderStateCSV = new UserRole { RoleName = SystemUserRole.OrderStateCSV, SystemRole = true };
            var OrderStatenew = new UserRole { RoleName = SystemUserRole.OrderStatenew, SystemRole = true };
            var OrderStateOverview = new UserRole { RoleName = SystemUserRole.OrderStateOverview, SystemRole = true };
            var OrderStateEdit = new UserRole { RoleName = SystemUserRole.OrderStateEdit, SystemRole = true };
            var OrderStateDelete = new UserRole { RoleName = SystemUserRole.OrderStateDelete, SystemRole = true };
            var DeviceStateCSV = new UserRole { RoleName = SystemUserRole.DeviceStateCSV, SystemRole = true };
            var DeviceStatenew = new UserRole { RoleName = SystemUserRole.DeviceStatenew, SystemRole = true };
            var DeviceStateOverview = new UserRole { RoleName = SystemUserRole.DeviceStateOverview, SystemRole = true };
            var DeviceStateEdit = new UserRole { RoleName = SystemUserRole.DeviceStateEdit, SystemRole = true };
            var DeviceStateDelete = new UserRole { RoleName = SystemUserRole.DeviceStateDelete, SystemRole = true };
            var Settings = new UserRole { RoleName = SystemUserRole.Settings, SystemRole = true };
            var ToBeTreatedMobileDeviceOverview = new UserRole { RoleName = SystemUserRole.ToBeTreatedMobileDeviceOverview, SystemRole = true };
            var ToBeTreatedMobileDeviceNew = new UserRole { RoleName = SystemUserRole.ToBeTreatedMobileDeviceNew, SystemRole = true };
            var ToBeTreatedMobileDeviceEdit = new UserRole { RoleName = SystemUserRole.ToBeTreatedMobileDeviceEdit, SystemRole = true };
            var ToBeTreatedMobileDeviceDelete = new UserRole { RoleName = SystemUserRole.ToBeTreatedMobileDeviceDelete, SystemRole = true };
            var ToBeTreatedMobileDeviceCSV = new UserRole { RoleName = SystemUserRole.ToBeTreatedMobileDeviceCSV, SystemRole = true };


            rgAdmin.Roles = new List<UserRole>();
            rgAdmin.Roles.Add(Settings);
            rgAdmin.Roles.Add(ToBeTreatedMobileDeviceOverview);
            rgAdmin.Roles.Add(ToBeTreatedMobileDeviceNew);
            rgAdmin.Roles.Add(ToBeTreatedMobileDeviceEdit);
            rgAdmin.Roles.Add(ToBeTreatedMobileDeviceDelete);
            rgAdmin.Roles.Add(ToBeTreatedMobileDeviceCSV);
            rgAdmin.Roles.Add(PoOverview);
            rgAdmin.Roles.Add(PoNew);
            rgAdmin.Roles.Add(PoEdit);
            rgAdmin.Roles.Add(PoDelete);
            rgAdmin.Roles.Add(PoCSV);
            rgAdmin.Roles.Add(PoViewItemLineDetail);
            rgAdmin.Roles.Add(ItemLineOverview);
            rgAdmin.Roles.Add(ItemLineNew);
            rgAdmin.Roles.Add(ItemLineEditItemLine);
            rgAdmin.Roles.Add(ItemLineDelete);
            rgAdmin.Roles.Add(ItemLineLinkDevice);
            rgAdmin.Roles.Add(ItemLineCSV);
            rgAdmin.Roles.Add(DeviceOverview);
            rgAdmin.Roles.Add(DeviceNew);
            rgAdmin.Roles.Add(DeviceEdit);
            rgAdmin.Roles.Add(DeviceDelete);
            rgAdmin.Roles.Add(DeviceCSV);
            rgAdmin.Roles.Add(DeviceLinkItemLine);
            rgAdmin.Roles.Add(DasBoardX);
            rgAdmin.Roles.Add(Usersverview);
            rgAdmin.Roles.Add(UsersNew);
            rgAdmin.Roles.Add(UsersEdit);
            rgAdmin.Roles.Add(UsersEditPasswordOnly);
            rgAdmin.Roles.Add(UsersDelete);
            rgAdmin.Roles.Add(UserAddLoginSite);
            rgAdmin.Roles.Add(UsersCSV);
            rgAdmin.Roles.Add(UserRolesView);
            rgAdmin.Roles.Add(UserRolesCSV);
            rgAdmin.Roles.Add(UserRolesGroupOverview);
            rgAdmin.Roles.Add(UserRoleGroupNew);
            rgAdmin.Roles.Add(UserRoleGroupEdit);
            rgAdmin.Roles.Add(UserRoleGroupDelete);
            rgAdmin.Roles.Add(UserRoleGroupAddRole);
            rgAdmin.Roles.Add(UserRoleGroupCSV);
            rgAdmin.Roles.Add(LanguageOverview);
            rgAdmin.Roles.Add(LanguageNew);
            rgAdmin.Roles.Add(LanguageEdit);
            rgAdmin.Roles.Add(LanguageDelete);
            rgAdmin.Roles.Add(LanguageCSV);
            rgAdmin.Roles.Add(TranslationOverview);
            rgAdmin.Roles.Add(TranslationNew);
            rgAdmin.Roles.Add(TranslationEdit);
            rgAdmin.Roles.Add(TranslationDelete);
            rgAdmin.Roles.Add(TranslationToggleMode);
            rgAdmin.Roles.Add(TranslationCSV);
            rgAdmin.Roles.Add(TranslationAllLanguages);
            rgAdmin.Roles.Add(StatesOverview);
            rgAdmin.Roles.Add(StatesNew);
            rgAdmin.Roles.Add(StatesEdit);
            rgAdmin.Roles.Add(StatesDelete);
            rgAdmin.Roles.Add(StatesCSV);
            rgAdmin.Roles.Add(DeviceStatesFlowOverview);
            rgAdmin.Roles.Add(DeviceStatesFlowNew);
            rgAdmin.Roles.Add(DeviceStatesFlowEdit);
            rgAdmin.Roles.Add(DeviceStatesFlowEditRemoveRoleGroups);
            rgAdmin.Roles.Add(DeviceStatesFlowEditAddRoleGroups);
            rgAdmin.Roles.Add(DeviceStatesFlowDelete);
            rgAdmin.Roles.Add(DeviceStatesFlowCSV);
            rgAdmin.Roles.Add(OrderStatesFlowOverview);
            rgAdmin.Roles.Add(OrderStatesFlowNew);
            rgAdmin.Roles.Add(OrderStatesFlowEdit);
            rgAdmin.Roles.Add(OrderStatesFlowEditRemoveRoleGroups);
            rgAdmin.Roles.Add(OrderStatesFlowEditAddRoleGroups);
            rgAdmin.Roles.Add(OrderStatesFlowDelete);
            rgAdmin.Roles.Add(OrderStatesFlowCSV);
            rgAdmin.Roles.Add(LoginSiteOverview);
            rgAdmin.Roles.Add(LoginSiteNew);
            rgAdmin.Roles.Add(LoginSiteEdit);
            rgAdmin.Roles.Add(LoginSiteDelete);
            rgAdmin.Roles.Add(LoginSiteCSV);
            rgAdmin.Roles.Add(LoginLicenceOverview);
            rgAdmin.Roles.Add(LoginLicenceNew);
            rgAdmin.Roles.Add(LoginLicenceEdit);
            rgAdmin.Roles.Add(LoginLicenceDelete);
            rgAdmin.Roles.Add(LoginLicenceCSV);
            rgAdmin.Roles.Add(RepairReasonOverview);
            rgAdmin.Roles.Add(RepairReasonSiteNew);
            rgAdmin.Roles.Add(RepairReasonEdit);
            rgAdmin.Roles.Add(RepairReasonDelete);
            rgAdmin.Roles.Add(RepairReasonCSV);
            rgAdmin.Roles.Add(PlatformOverview);
            rgAdmin.Roles.Add(PlatformSiteNew);
            rgAdmin.Roles.Add(PlatformEdit);
            rgAdmin.Roles.Add(PlatformDelete);
            rgAdmin.Roles.Add(PlatformCSV);
            rgAdmin.Roles.Add(FlocIdOverview);
            rgAdmin.Roles.Add(FlocIdSiteNew);
            rgAdmin.Roles.Add(FlocIdEdit);
            rgAdmin.Roles.Add(FlocIdDelete);
            rgAdmin.Roles.Add(FlocIdCSV);
            rgAdmin.Roles.Add(DeviceTypeOverview);
            rgAdmin.Roles.Add(DeviceTypeSiteNew);
            rgAdmin.Roles.Add(DeviceTypeEdit);
            rgAdmin.Roles.Add(DeviceTypeDelete);
            rgAdmin.Roles.Add(DeviceTypeCSV);
            rgAdmin.Roles.Add(ProductTypeOverview);
            rgAdmin.Roles.Add(ProductTypeSiteNew);
            rgAdmin.Roles.Add(ProductTypeEdit);
            rgAdmin.Roles.Add(ProductTypeDelete);
            rgAdmin.Roles.Add(ProductTypeCSV);
            rgAdmin.Roles.Add(CSVImport);
            rgAdmin.Roles.Add(OrderStateCSV);
            rgAdmin.Roles.Add(OrderStatenew);
            rgAdmin.Roles.Add(OrderStateOverview);
            rgAdmin.Roles.Add(OrderStateEdit);
            rgAdmin.Roles.Add(OrderStateDelete);
            rgAdmin.Roles.Add(DeviceStateCSV);
            rgAdmin.Roles.Add(DeviceStatenew);
            rgAdmin.Roles.Add(DeviceStateOverview);
            rgAdmin.Roles.Add(DeviceStateEdit);
            rgAdmin.Roles.Add(DeviceStateDelete);


            context.UserRoles.AddOrUpdate(r => r.RoleName,
                    Settings,
                    ToBeTreatedMobileDeviceOverview,
                    ToBeTreatedMobileDeviceNew,
                    ToBeTreatedMobileDeviceEdit,
                    ToBeTreatedMobileDeviceDelete,
                    ToBeTreatedMobileDeviceCSV,
                    PoOverview,
                    PoNew,
                    PoEdit,
                    PoDelete,
                    PoCSV,
                    PoViewItemLineDetail,
                    ItemLineOverview,
                    ItemLineNew,
                    ItemLineEditItemLine,
                    ItemLineDelete,
                    ItemLineLinkDevice,
                    ItemLineCSV,
                    DeviceOverview,
                    DeviceNew,
                    DeviceEdit,
                    DeviceDelete,
                    DeviceCSV,
                    DeviceLinkItemLine,
                    DasBoardX,
                    Usersverview,
                    UsersNew,
                    UsersEdit,
                    UsersEditPasswordOnly,
                    UsersDelete,
                    UserAddLoginSite,
                    UsersCSV,
                    UserRolesView,
                    UserRolesCSV,
                    UserRolesGroupOverview,
                    UserRoleGroupNew,
                    UserRoleGroupEdit,
                    UserRoleGroupDelete,
                    UserRoleGroupAddRole,
                    UserRoleGroupCSV,
                    LanguageOverview,
                    LanguageNew,
                    LanguageEdit,
                    LanguageDelete,
                    LanguageCSV,
                    TranslationOverview,
                    TranslationNew,
                    TranslationEdit,
                    TranslationDelete,
                    TranslationToggleMode,
                    TranslationCSV,
                    TranslationAllLanguages,
                    StatesOverview,
                    StatesNew,
                    StatesEdit,
                    StatesDelete,
                    StatesCSV,
                    DeviceStatesFlowOverview,
                    DeviceStatesFlowNew,
                    DeviceStatesFlowEdit,
                    DeviceStatesFlowEditRemoveRoleGroups,
                    DeviceStatesFlowEditAddRoleGroups,
                    DeviceStatesFlowDelete,
                    DeviceStatesFlowCSV,
                    OrderStatesFlowOverview,
                    OrderStatesFlowNew,
                    OrderStatesFlowEdit,
                    OrderStatesFlowEditRemoveRoleGroups,
                    OrderStatesFlowEditAddRoleGroups,
                    OrderStatesFlowDelete,
                    OrderStatesFlowCSV,
                    LoginSiteOverview,
                    LoginSiteNew,
                    LoginSiteEdit,
                    LoginSiteDelete,
                    LoginSiteCSV,
                    LoginLicenceOverview,
                    LoginLicenceNew,
                    LoginLicenceEdit,
                    LoginLicenceDelete,
                    LoginLicenceCSV,
                    RepairReasonOverview,
                    RepairReasonSiteNew,
                    RepairReasonEdit,
                    RepairReasonDelete,
                    RepairReasonCSV,
                    PlatformOverview,
                    PlatformSiteNew,
                    PlatformEdit,
                    PlatformDelete,
                    PlatformCSV,
                    FlocIdOverview,
                    FlocIdSiteNew,
                    FlocIdEdit,
                    FlocIdDelete,
                    FlocIdCSV,
                    DeviceTypeOverview,
                    DeviceTypeSiteNew,
                    DeviceTypeEdit,
                    DeviceTypeDelete,
                    DeviceTypeCSV,
                    ProductTypeOverview,
                    ProductTypeSiteNew,
                    ProductTypeEdit,
                    ProductTypeDelete,
                    ProductTypeCSV,
                    CSVImport,
                    OrderStateCSV,
                    OrderStatenew,
                    OrderStateOverview,
                    OrderStateEdit,
                    OrderStateDelete,
                    DeviceStateCSV,
                    DeviceStatenew,
                    DeviceStateOverview,
                    DeviceStateEdit,
                    DeviceStateDelete
                );
            #endregion

            context.UserRoleGroups.AddOrUpdate(urg => urg.Name,
                rgAdmin,
                new UserRoleGroup { Name = "LoginCC" },
                new UserRoleGroup { Name = "KeyUser" },
                new UserRoleGroup { Name = "Operation Coordinator" }
             );

            context.Users.AddOrUpdate(r => r.Email,
                admin
             );


            #region Translation

            context.Translations.AddOrUpdate(t => new { t.LanguageId, t.Group, t.Keyword },
               new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Users", Value = "Gebruikers" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Devices", Value = "Mobiele toestellen" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Admin", Value = "Beheer" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "OrderItemlist", Value = "Bestelling details" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Dashboard", Value = "Dashboard" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "PurchaseOrderLists", Value = "Bestellingen" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Languages", Value = "Talen" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Translations", Value = "Vertalingen" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "States", Value = "Statussen" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "RoleGroups", Value = "Rechten groepen" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Flow", Value = "Status beheer" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "UserRoles", Value = "Gebruikers rechten" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "OrderStateChanges", Value = "Bestelling status overgang" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "LoginSites", Value = "Login posten" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "RepairReason", Value = "Herstelling reden" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Platform", Value = "Platform" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "CSVImport", Value = "CSV import" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "StateChanges", Value = "Toestel status overgang" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "Lists", Value = "Lijsten" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "ProductType", Value = "Product types" },
                new Translation { LanguageId = 1, Group = "SideNav", Keyword = "DeviceType", Value = "Toestel types" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "PurchaseOrder", Value = "Bestellingen" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "list", Value = "Overzicht" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "purchaseOrders", Value = "Bestellingen" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "Overview", Value = "Overzicht" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "AddNew", Value = "Nieuw" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "HeaderPurchaseOrderNumber", Value = "Bestelling nr." },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "HeaderOrderDate", Value = "Bestel datum" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "HeaderAnnulationDate", Value = "Eind datum" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "TablePaginationOf", Value = "van" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "BtnDownloadCSV", Value = "CSV" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "TablePaginationItems", Value = "rijen" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "Of", Value = "van" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "First", Value = "Eerste" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "Last", Value = "Laatste" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "PurchaseOrderNumber", Value = "Bestelling nr." },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "CreatePanelTitle", Value = "Bestelling aanmaken" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "BtnSave", Value = "Opslaan" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "new", Value = "Nieuw" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "OrderDate", Value = "Betsel datum" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "BtnCancel", Value = "Annuleren" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "OrderDateOrderRequired", Value = "Bestel datum verplicht" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "PurchaseOrderNumberrRequired", Value = "Betel nr verplicht" },
                new Translation { LanguageId = 1, Group = "Network", Keyword = "Error", Value = "Fout" },
                new Translation { LanguageId = 1, Group = "OrderItems", Keyword = "OrderItemsForPurchaseOrder", Value = "Betstelling details" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "detail", Value = "Detail" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "DeleteItemAction", Value = "Verwijder selectie" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "HeaderQuantityOfProducts", Value = "Hoeveelheid" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "HeaderDeviceType", Value = "Toestel type" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "HeaderType", Value = "Product type" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "Delete", Value = "Verwijder" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "AddOrderItem", Value = "Nieuw bestel detail" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "Edit", Value = "Bewerken" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "Detail", Value = "Detail" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "AnnulationDate", Value = "Annulatie datum" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "edit", Value = "Bewerken" },
                new Translation { LanguageId = 1, Group = "PurchaseOrder", Keyword = "EditPanelTitle", Value = "Bewerk bestelling" },
                new Translation { LanguageId = 1, Group = "Modals", Keyword = "CreateOrderItemTitle", Value = "Nieuw Bestel Detail" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "ItemLine", Value = "Bestel detail nr." },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "DeliveryToOperations", Value = "Levering aan operaties" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "DeliveryOfSupplier", Value = "Levering van Leverancier" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "CostCenter", Value = "Kost Center" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "QuantityOfProducts", Value = "Hoeveelheid" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "CostCenterRequired", Value = "Kost Center verplicht" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "ItemLineRequired", Value = "Bestel detail nr. verplicht" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "ProductType", Value = "Product type" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "QuantityOfProductsRequired", Value = "Hoeveelheid verplicht" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "ProductTypeRequired", Value = "Product type veplicht" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "orderItems", Value = "Bestel details" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "OrderItem", Value = "Bestel detail" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "HeaderSupplied", Value = "Geleverd" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "HeaderPurchaseOrderNumber", Value = "Bestelling nr." },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "HeaderCanceled", Value = "Geannuleerd" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "MobileDevices", Value = "Mobiele toestellen" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "HeadersType", Value = "Type" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "mobileDevices", Value = "Mobieme toestellen" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "HeadersReference", Value = "IMEI" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "HeadersCurrentState", Value = "Huidige status" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "HeadersLastStateDate", Value = "Laatste staus wijziging" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "HeadersDeviceName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "HeadersLoginSite", Value = "Login site" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "PanelTitle", Value = "LWP configuratie" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "Name", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "PlatformRequired", Value = "Platform verplicht" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "Platform", Value = "Platform" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "NamePlaceHolder", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "CreatePanelTitle", Value = "Mobiel toestel" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "PhoneNumber", Value = "Telefoon nr." },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "ReferencePlaceHolder", Value = "IMEI" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "ReferenceRequired", Value = "IEMEI verplicht" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "PhoneNumberHolder", Value = "Telefoon nr." },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "Reference", Value = "IMEI" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "LoginSite", Value = "Login site" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "TypeRequired", Value = "Type verplicht" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "Type", Value = "Type" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "MovementDetectionActivated", Value = "Bewegings detectie actief" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "PhoneNumbersForTelephoneAlarm", Value = "Oproepnummers bij alarm" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "TelephoneAlarmActivated", Value = "Telefoon bij alarm actief" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "PanicButtonActivated", Value = "Paniek knop actief" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "TimeBeforeManDownAlarmInSeconds", Value = "Tijd voor Man Down alarm" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "AngleOfManDownDetection", Value = "Hoeh van Man Down alarm" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "TimeBeforeMovementAlarmInSeconds", Value = "Tijd voor bewegings alarm" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "SchockAlarmActivated", Value = "Impact alarm actief" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "ManDownAlarmActivated", Value = "Man Down alarm actief" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "FallAlarmActivated", Value = "Val alarm actief" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "TimerAlarmActivated", Value = "Timer actief" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "SendAlarmToExternalAlarmReciverActivated", Value = "Stuur alarm naar externe reciever" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "UniqueIdentifierToSendToExternalAlarmReciever", Value = "Unieke externe alarm reciever nr." },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "TimeBeforeTimerAlarmInSeconds", Value = "Tijd voor timer alarm" },
                new Translation { LanguageId = 1, Group = "LwpSetting", Keyword = "ExitGeofenceAreaWhenUserSignsOff", Value = "Uit geofence zone als gebruiker afmeld" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "Users", Value = "Gebruikers" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "HeadersName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "HeadersLanguage", Value = "Taal" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "HeadersFirstName", Value = "Voornaam" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "users", Value = "Gebruikers" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "HeadersEmail", Value = "Email" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "HeadersRoleGroup", Value = "Rechten groep" },
                new Translation { LanguageId = 1, Group = "UserRole", Keyword = "HeaderRoleName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "UserRole", Keyword = "HeaderDescription", Value = "Beschrijving" },
                new Translation { LanguageId = 1, Group = "UserRoleGroups", Keyword = "SelectedUserRoles", Value = "Overzicht toegangsrechten" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "FirstName", Value = "Voornaam" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "Name", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "Language", Value = "Taal" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "Email", Value = "Email" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "RoleGroup", Value = "Rechten groep" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "NameRequired", Value = "Naam verplicht" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "EditPanelTitle", Value = "Bewerk gebruiker" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "RoleGroupRequired", Value = "Rechten groep verplicht" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "FirstNameRequired", Value = "Voornaam verplicht" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "LanguageRequired", Value = "Taal verplicht" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "CreatePanelTitle", Value = "Nieuwe gebruiker" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "PasswordAtLeast8Characters", Value = "Paswoord minimum 8 karakters" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "PasswordRepeat", Value = "Herhaal paswoord" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "PasswordRepeatRequired", Value = "Herhaling paswoord verplicht" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "EmailValidRequired", Value = "Geldige Email verplicht" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "Password", Value = "Paswoord" },
                new Translation { LanguageId = 1, Group = "User", Keyword = "PasswordRepeatMustMatch", Value = "Wahtwoorden komen niet overeen" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "userRoles", Value = "Gebruiker rechten" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "UserRoles", Value = "Gebruiker rechten" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "UserRoleGroup", Value = "Rechten groepen" },
                new Translation { LanguageId = 1, Group = "UserRoleGroup", Keyword = "HeaderName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "userRoleGroups", Value = "Rechten groepen" },
                new Translation { LanguageId = 1, Group = "UserRoleGroups", Keyword = "Name", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "UserRoleGroup", Keyword = "CreatePanelTitle", Value = "Nieuw rechten groep" },
                new Translation { LanguageId = 1, Group = "UserRoleGroups", Keyword = "NameRequired", Value = "Naam is verplicht" },
                new Translation { LanguageId = 1, Group = "UserRole", Keyword = "DeleteRoleAction", Value = "Verwijder selectie" },
                new Translation { LanguageId = 1, Group = "UserRoleGroups", Keyword = "AddRole", Value = "Voeg recht toe" },
                new Translation { LanguageId = 1, Group = "UserRoleGroup", Keyword = "Name", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "Modals", Keyword = "AddRoleToGroup", Value = "Voeg recht toe tot de groep" },
                new Translation { LanguageId = 1, Group = "AddRoleModal", Keyword = "Role", Value = "Recht" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "languages", Value = "Talen" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "HeaderLanguage", Value = "Talen" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "Languages", Value = "Talen" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "HeaderShortCode", Value = "Taal code" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "ShortCode", Value = "Taal code" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "Language", Value = "Taal" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "EditPanelTitle", Value = "Bewerk taal" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "ShortCodeRequired", Value = "Taal code verplicht" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "LanguageRequired", Value = "Taal verplicht" },
                new Translation { LanguageId = 1, Group = "Language", Keyword = "CreatePanelTitle", Value = "Nieuwe taal" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "DisableTranslationBtn", Value = "Schakel vertaling uit" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "translations", Value = "Vertalingen" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "HeaderLanguage", Value = "Taal" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "Translations", Value = "Vertaling" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "HeaderKeyword", Value = "Sleutel woord" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "HeaderGroup", Value = "Groep" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "HeaderValue", Value = "Vertaling" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "Language", Value = "Taal" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "Value", Value = "Waarde" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "Group", Value = "Groep" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "Keyword", Value = "Sleutel woord" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "EditPanelTitle", Value = "Bewerk vertaling" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "GroupRequired", Value = "Groep verplicht" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "ValueRequired", Value = "Waarde verplicht" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "LanguageRequired", Value = "Taal verplicht" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "KeywordRequired", Value = "Sleutel woord verplicht" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "EnableTranslationBtn", Value = "Shakel vertalingen in" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "states", Value = "Statussen" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "HeaderDescription", Value = "Beschrijving" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "States", Value = "Statussen" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "HeaderName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "Name", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "Description", Value = "Beschrijving" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "DescriptionRequired", Value = "Beschrijving verplicht" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "NameRequired", Value = "Naam verplicht" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "CreatePanelTitle", Value = "Nieuwe status" },
                new Translation { LanguageId = 1, Group = "State", Keyword = "EditPanelTitle", Value = "Bewerk status" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "StateChanges", Value = "Toestel status overgangen" },
                new Translation { LanguageId = 1, Group = "StateChange", Keyword = "HeaderStateTo", Value = "Naar status" },
                new Translation { LanguageId = 1, Group = "StateChange", Keyword = "HeaderStateFrom", Value = "Van status" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "stateChanges", Value = "Toestel status overgang" },
                new Translation { LanguageId = 1, Group = "OrderStateChange", Keyword = "HeaderProductType", Value = "Product type" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "OrderStateChanges", Value = "Bestelling status overgangen" },
                new Translation { LanguageId = 1, Group = "OrderStateChange", Keyword = "HeaderStateFrom", Value = "Van status" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "orderStateChanges", Value = "Bestelling status overgangen" },
                new Translation { LanguageId = 1, Group = "OrderStateChange", Keyword = "HeaderStateTo", Value = "Naar status" },
                new Translation { LanguageId = 1, Group = "StateChange", Keyword = "CreatePanelTitle", Value = "Nieuwe status overgang" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "StateFromRequired", Value = "Van status verplicht" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "StateToRequired", Value = "Naar status verplicht" },
                new Translation { LanguageId = 1, Group = "StateChange", Keyword = "StateFrom", Value = "Van status" },
                new Translation { LanguageId = 1, Group = "StateChange", Keyword = "StateTo", Value = "Naar status" },
                new Translation { LanguageId = 1, Group = "StateChange", Keyword = "SelectedUserRoleGroups", Value = "rechten groepen met toegang tot overgang" },
                new Translation { LanguageId = 1, Group = "UserRoleGroups", Keyword = "AddRoleGroup", Value = "Nieuw" },
                new Translation { LanguageId = 1, Group = "UserRoleGroup", Keyword = "DeleteGroupAction", Value = "Verwijder selectie" },
                new Translation { LanguageId = 1, Group = "Modals", Keyword = "AddRoleGroupToStateChange", Value = "Voeg rechten groep to tot status overgang " },
                new Translation { LanguageId = 1, Group = "AddRoleGroupModal", Keyword = "UserRoleGroup", Value = "Rechten groep" },
                new Translation { LanguageId = 1, Group = "OrderStateChange", Keyword = "CreatePanelTitle", Value = "Nieuwe bestelling status overgang" },
                new Translation { LanguageId = 1, Group = "OrderStateChange", Keyword = "StateFrom", Value = "Van status" },
                new Translation { LanguageId = 1, Group = "Translation", Keyword = "ProductTypeRequired", Value = "Product type verplicht" },
                new Translation { LanguageId = 1, Group = "OrderStateChange", Keyword = "ProductType", Value = "Product type" },
                new Translation { LanguageId = 1, Group = "OrderStateChange", Keyword = "StateTo", Value = "Naar status" },
                new Translation { LanguageId = 1, Group = "StateChange", Keyword = "EditPanelTitle", Value = "Bewerk status overgang" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "LoginSites", Value = "Login sites" },
                new Translation { LanguageId = 1, Group = "LoginSite", Keyword = "HeaderSiteName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "loginSites", Value = "Login sites" },
                new Translation { LanguageId = 1, Group = "LoginSite", Keyword = "CreatePanelTitle", Value = "Nieuwe login site" },
                new Translation { LanguageId = 1, Group = "LoginSite", Keyword = "SiteNameRequired", Value = "Naam verplicht" },
                new Translation { LanguageId = 1, Group = "LoginSite", Keyword = "SiteName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "LoginSite", Keyword = "EditPanelTitle", Value = "Bewerk Login site" },
                new Translation { LanguageId = 1, Group = "RepairReason", Keyword = "HeaderReason", Value = "Herstelling reden" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "RepairReasons", Value = "Herstelling reden" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "repairReasons", Value = "Herstelling reden" },
                new Translation { LanguageId = 1, Group = "RepairReason", Keyword = "ReasonRequired", Value = "Reden verplicht" },
                new Translation { LanguageId = 1, Group = "RepairReason", Keyword = "CreatePanelTitle", Value = "Nieuw repair reden" },
                new Translation { LanguageId = 1, Group = "RepairReason", Keyword = "Reason", Value = "Repair reden" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "platforms", Value = "Platfrom" },
                new Translation { LanguageId = 1, Group = "Platform", Keyword = "HeaderPlatform", Value = "PLatform" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "Platform", Value = "Platform" },
                new Translation { LanguageId = 1, Group = "Platform", Keyword = "CreatePanelTitle", Value = "Nieuw platform" },
                new Translation { LanguageId = 1, Group = "Platform", Keyword = "PlatformRequired", Value = "Platform verplicht" },
                new Translation { LanguageId = 1, Group = "Platform", Keyword = "Platform", Value = "PLatform" },
                new Translation { LanguageId = 1, Group = "Platform", Keyword = "PlatformName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "Platform", Keyword = "EditPanelTitle", Value = "Bewerk platform" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "deviceTypes", Value = "Toestel types" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "DeviceTypes", Value = "Toestel types" },
                new Translation { LanguageId = 1, Group = "DeviceType", Keyword = "HeaderTypeName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "DeviceType", Keyword = "HeaderLwpSettingPossible", Value = "LWP mogelijk" },
                new Translation { LanguageId = 1, Group = "DeviceType", Keyword = "CreatePanelTitle", Value = "Nieuw type" },
                new Translation { LanguageId = 1, Group = "DeviceType", Keyword = "TypeName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "DeviceType", Keyword = "LwpSettingPossible", Value = "LWP mogelijk" },
                new Translation { LanguageId = 1, Group = "DeviceType", Keyword = "TypeNameRequired", Value = "Naam verplicht" },
                new Translation { LanguageId = 1, Group = "DeviceType", Keyword = "EditPanelTitle", Value = "Bewerk toestel type" },
                new Translation { LanguageId = 1, Group = "Title", Keyword = "ProductTypes", Value = "Product types" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "HeaderHeaderTypeName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "Breadcrumb", Keyword = "productTypes", Value = "Type" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "HeaderTypeName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "HeaderHasOrderStates", Value = "Bestelling statussen" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "CreatePanelTitle", Value = "Nieuw product type" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "HasOrderStates", Value = "Heeft bestelling statussen" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "TypeNameRequired", Value = "Naam verplicht" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "DeviceTypeRequired", Value = "Toestel type verplicht" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "TypeName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "ProductType", Keyword = "EditPanelTitle", Value = "Bewerk product type" },
                new Translation { LanguageId = 1, Group = "Login", Keyword = "Password", Value = "Paswoord" },
                new Translation { LanguageId = 1, Group = "Login", Keyword = "Username", Value = "Gebruikersnaam" },
                new Translation { LanguageId = 1, Group = "Login", Keyword = "SubTitle", Value = "Aanmelden" },
                new Translation { LanguageId = 1, Group = "Login", Keyword = "Title", Value = "G4S OLDMan" },
                new Translation { LanguageId = 1, Group = "Login", Keyword = "BtnLogin", Value = "Aanmelden" },
                new Translation { LanguageId = 1, Group = "Authentication", Keyword = "DeniedIsuffientPrivileges", Value = "Onvoldoende rechten" },
                new Translation { LanguageId = 1, Group = "Authentication", Keyword = "AccesDenied", Value = "Toegang geweigerd" },
                new Translation { LanguageId = 1, Group = "Authentication", Keyword = "DeniedAdminContact", Value = "Contacteer een admin" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "DeviceName", Value = "Naam" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "AnnulationDate", Value = "Annulatie datum" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "StateDetail", Value = "Satus detail" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "LastStateDate", Value = "Laatste status weiziging datum" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "Country", Value = "Land" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "CurrentState", Value = "Huidige status" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "StateChangePanelTitle", Value = "Status wijzigen" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "DeviceStateHistoryTimeline", Value = "Status historiek" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "LwpSettingDetail", Value = "Bewerk LWP setting" },
                new Translation { LanguageId = 1, Group = "MobileDevice", Keyword = "EditPanelTitle", Value = "Bewerk mobiel toestel" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "Type", Value = "Type" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "DeviceType", Value = "Toestel type" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "PurchaseOrderTitle", Value = "Bestelling" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "DeviceLink", Value = "Toestel link" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "CurrentState", Value = "Huidige status" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "LastStateDate", Value = "Laaste status" },
                new Translation { LanguageId = 1, Group = "General", Keyword = "OrderItemStateHistoryTimeline", Value = "Bestelling status historiek" },
                new Translation { LanguageId = 1, Group = "OrderItemStateHistory", Keyword = "ChangedBy", Value = "Gewijzigd door" },
                new Translation { LanguageId = 1, Group = "OrderItemStateHistory", Keyword = "ChangedOn", Value = "Gewijzigd op" },
                new Translation { LanguageId = 1, Group = "OrderItem", Keyword = "EditPanelTitle", Value = "Bewerk bestel detail" },
                new Translation { LanguageId = 1, Group = "DeviceStateHistory", Keyword = "ChangedOn", Value = "Gewijzigd op" },
                new Translation { LanguageId = 1, Group = "DeviceStateHistory", Keyword = "ChangedBy", Value = "Gewijzigd door" }

            );
            #endregion

            context.Platform.AddOrUpdate(p => p.PlatformName,
                new Platform { PlatformName = "Nationaal" },
                new Platform { PlatformName = "Aviation" },
                new Platform { PlatformName = "Europese" },
                new Platform { PlatformName = "GSK" }
            );

            var productTypeDevice = new ProductType { TypeName = "Device", HasOrderStates = true, DeviceTypeRequired = true };
            context.ProductType.AddOrUpdate(p => p.TypeName,
                new ProductType { TypeName = "License", HasOrderStates = true, DeviceTypeRequired = false },
                new ProductType { TypeName = "Tags RFID", HasOrderStates = true, DeviceTypeRequired = false },
                new ProductType { TypeName = "Tags NFC", HasOrderStates = true, DeviceTypeRequired = false },
                productTypeDevice
            );

            #region States
            StateKind deviceKind = new StateKind { Name = "Device" };
            StateKind orderKind = new StateKind { Name = "Order" };

            context.StateKinds.AddOrUpdate(sk => sk.Name,
                deviceKind,
                orderKind);

            var Ordered = new State { Name = "Ordered", Description = "Ordered", Kind = orderKind };
            var DeliveredLoginCC = new State { Name = "Delivered to Login CC", Description = "Delivered to Login CC", Kind = orderKind };
            var ItemConfigurated = new State { Name = "Item is configurated", Description = "Item is configurated", Kind = orderKind };
            var ItemTested = new State { Name = "Item is tested", Description = "Item is tested", Kind = orderKind };
            var ItemSentOpperations = new State { Name = "Item is sent to Opperations", Description = "Item is sent to Opperations", Kind = orderKind };
            var ItemOperational = new State { Name = "Item is operational", Description = "Item is operational", Kind = orderKind };
            var Stopped = new State { Name = "Stopped", Description = "Stopped", Kind = orderKind };
            var SentLoginCC = new State { Name = "Sent to Login CC", Description = "Sent to Login CC", Kind = orderKind };

            context.States.AddOrUpdate(s => s.Name,
                Ordered,
                DeliveredLoginCC,
                ItemConfigurated,
                ItemTested,
                ItemSentOpperations,
                ItemOperational,
                Stopped,
                SentLoginCC
            );

            context.OrderStateChanges.AddOrUpdate(
                new OrderStateChange { StateTo = Ordered, ProductType = productTypeDevice },
                new OrderStateChange { StateTo = DeliveredLoginCC, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = Ordered, StateTo = DeliveredLoginCC, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = Ordered, StateTo = ItemConfigurated, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = Ordered, StateTo = ItemTested, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = Ordered, StateTo = ItemSentOpperations, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = DeliveredLoginCC, StateTo = ItemConfigurated, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = DeliveredLoginCC, StateTo = ItemTested, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = DeliveredLoginCC, StateTo = ItemSentOpperations, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = ItemConfigurated, StateTo = ItemTested, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = ItemConfigurated, StateTo = ItemSentOpperations, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = ItemTested, StateTo = ItemSentOpperations, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = ItemSentOpperations, StateTo = ItemOperational, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = ItemOperational, StateTo = Stopped, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = ItemOperational, StateTo = Stopped, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = Stopped, StateTo = DeliveredLoginCC, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = Stopped, StateTo = SentLoginCC, ProductType = productTypeDevice },
                new OrderStateChange { StateFrom = DeliveredLoginCC, StateTo = SentLoginCC, ProductType = productTypeDevice }
           );
            #endregion

            context.DeviceType.AddOrUpdate(d => d.TypeName,
                new DeviceType { TypeName = "Active Guard", LwpSettingPossible = false },
                new DeviceType { TypeName = "Titan", LwpSettingPossible = true },
                new DeviceType { TypeName = "Sonim", LwpSettingPossible = true }
            );

            context.LoginSites.AddOrUpdate(ls => ls.SiteName,
                new LoginSite { SiteName = "Demo" },
                new LoginSite { SiteName = "SandBox" }
            );

            context.SaveChanges();
        }
    }
}
