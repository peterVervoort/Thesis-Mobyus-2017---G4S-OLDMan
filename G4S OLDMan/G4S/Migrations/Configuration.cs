namespace G4S.Migrations
{
    using Entities.Enums;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<G4S.IdentityModels.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(G4S.IdentityModels.ApplicationDbContext context)
        {
            var passwordHash = new PasswordHasher();
            context.Users.AddOrUpdate(r => r.Email,
                new IdentityModels.ApplicationUser { Email = "admin@g4s.be", UserName = "admin@g4s.be", PasswordHash = passwordHash.HashPassword("Tettekop123"), SecurityStamp = "RandomStringGoesHere" });

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = SystemUserRole.Settings },
                new IdentityRole { Name = SystemUserRole.PoOverview },
                new IdentityRole { Name = SystemUserRole.PoNew },
                new IdentityRole { Name = SystemUserRole.PoEdit },
                new IdentityRole { Name = SystemUserRole.PoDelete },
                new IdentityRole { Name = SystemUserRole.PoCSV },
                new IdentityRole { Name = SystemUserRole.PoViewItemLineDetail },
                new IdentityRole { Name = SystemUserRole.ItemLineOverview },
                new IdentityRole { Name = SystemUserRole.ItemLineNew },
                new IdentityRole { Name = SystemUserRole.ItemLineEditItemLine },
                new IdentityRole { Name = SystemUserRole.ItemLineDelete },
                new IdentityRole { Name = SystemUserRole.ItemLineLinkDevice },
                new IdentityRole { Name = SystemUserRole.ItemLineCSV },
                new IdentityRole { Name = SystemUserRole.DeviceOverview },
                new IdentityRole { Name = SystemUserRole.DeviceNew },
                new IdentityRole { Name = SystemUserRole.DeviceEdit },
                new IdentityRole { Name = SystemUserRole.DeviceDelete },
                new IdentityRole { Name = SystemUserRole.DeviceCSV },
                new IdentityRole { Name = SystemUserRole.DeviceLinkItemLine },
                new IdentityRole { Name = SystemUserRole.DasBoardX },
                new IdentityRole { Name = SystemUserRole.Usersverview },
                new IdentityRole { Name = SystemUserRole.UsersNew },
                new IdentityRole { Name = SystemUserRole.UsersEdit },
                new IdentityRole { Name = SystemUserRole.UsersEditPasswordOnly },
                new IdentityRole { Name = SystemUserRole.UsersDelete },
                new IdentityRole { Name = SystemUserRole.UserAddLoginSite },
                new IdentityRole { Name = SystemUserRole.UsersCSV },
                new IdentityRole { Name = SystemUserRole.UserRolesView },
                new IdentityRole { Name = SystemUserRole.UserRolesCSV },
                new IdentityRole { Name = SystemUserRole.UserRolesGroupOverview },
                new IdentityRole { Name = SystemUserRole.UserRoleGroupNew },
                new IdentityRole { Name = SystemUserRole.UserRoleGroupEdit },
                new IdentityRole { Name = SystemUserRole.UserRoleGroupDelete },
                new IdentityRole { Name = SystemUserRole.UserRoleGroupAddRole },
                new IdentityRole { Name = SystemUserRole.UserRoleGroupCSV },
                new IdentityRole { Name = SystemUserRole.LanguageOverview },
                new IdentityRole { Name = SystemUserRole.LanguageNew },
                new IdentityRole { Name = SystemUserRole.LanguageEdit },
                new IdentityRole { Name = SystemUserRole.LanguageDelete },
                new IdentityRole { Name = SystemUserRole.LanguageCSV },
                new IdentityRole { Name = SystemUserRole.TranslationOverview },
                new IdentityRole { Name = SystemUserRole.TranslationNew },
                new IdentityRole { Name = SystemUserRole.TranslationEdit },
                new IdentityRole { Name = SystemUserRole.TranslationDelete },
                new IdentityRole { Name = SystemUserRole.TranslationToggleMode },
                new IdentityRole { Name = SystemUserRole.TranslationCSV },
                new IdentityRole { Name = SystemUserRole.TranslationAllLanguages },
                new IdentityRole { Name = SystemUserRole.StatesOverview },
                new IdentityRole { Name = SystemUserRole.StatesNew },
                new IdentityRole { Name = SystemUserRole.StatesEdit },
                new IdentityRole { Name = SystemUserRole.StatesDelete },
                new IdentityRole { Name = SystemUserRole.StatesCSV },
                new IdentityRole { Name = SystemUserRole.DeviceStatesFlowOverview },
                new IdentityRole { Name = SystemUserRole.DeviceStatesFlowNew },
                new IdentityRole { Name = SystemUserRole.DeviceStatesFlowEdit },
                new IdentityRole { Name = SystemUserRole.DeviceStatesFlowEditRemoveRoleGroups },
                new IdentityRole { Name = SystemUserRole.DeviceStatesFlowEditAddRoleGroups },
                new IdentityRole { Name = SystemUserRole.DeviceStatesFlowDelete },
                new IdentityRole { Name = SystemUserRole.DeviceStatesFlowCSV },
                new IdentityRole { Name = SystemUserRole.OrderStatesFlowOverview },
                new IdentityRole { Name = SystemUserRole.OrderStatesFlowNew },
                new IdentityRole { Name = SystemUserRole.OrderStatesFlowEdit },
                new IdentityRole { Name = SystemUserRole.OrderStatesFlowEditRemoveRoleGroups },
                new IdentityRole { Name = SystemUserRole.OrderStatesFlowEditAddRoleGroups },
                new IdentityRole { Name = SystemUserRole.OrderStatesFlowDelete },
                new IdentityRole { Name = SystemUserRole.OrderStatesFlowCSV },
                new IdentityRole { Name = SystemUserRole.LoginSiteOverview },
                new IdentityRole { Name = SystemUserRole.LoginSiteNew },
                new IdentityRole { Name = SystemUserRole.LoginSiteEdit },
                new IdentityRole { Name = SystemUserRole.LoginSiteDelete },
                new IdentityRole { Name = SystemUserRole.LoginSiteCSV },
                new IdentityRole { Name = SystemUserRole.LoginLicenceOverview },
                new IdentityRole { Name = SystemUserRole.LoginLicenceNew },
                new IdentityRole { Name = SystemUserRole.LoginLicenceEdit },
                new IdentityRole { Name = SystemUserRole.LoginLicenceDelete },
                new IdentityRole { Name = SystemUserRole.LoginLicenceCSV },
                new IdentityRole { Name = SystemUserRole.RepairReasonOverview },
                new IdentityRole { Name = SystemUserRole.RepairReasonSiteNew },
                new IdentityRole { Name = SystemUserRole.RepairReasonEdit },
                new IdentityRole { Name = SystemUserRole.RepairReasonDelete },
                new IdentityRole { Name = SystemUserRole.RepairReasonCSV },
                new IdentityRole { Name = SystemUserRole.PlatformOverview },
                new IdentityRole { Name = SystemUserRole.PlatformSiteNew },
                new IdentityRole { Name = SystemUserRole.PlatformEdit },
                new IdentityRole { Name = SystemUserRole.PlatformDelete },
                new IdentityRole { Name = SystemUserRole.PlatformCSV },
                new IdentityRole { Name = SystemUserRole.FlocIdOverview },
                new IdentityRole { Name = SystemUserRole.FlocIdSiteNew },
                new IdentityRole { Name = SystemUserRole.FlocIdEdit },
                new IdentityRole { Name = SystemUserRole.FlocIdDelete },
                new IdentityRole { Name = SystemUserRole.FlocIdCSV },
                new IdentityRole { Name = SystemUserRole.DeviceTypeOverview },
                new IdentityRole { Name = SystemUserRole.DeviceTypeSiteNew },
                new IdentityRole { Name = SystemUserRole.DeviceTypeEdit },
                new IdentityRole { Name = SystemUserRole.DeviceTypeDelete },
                new IdentityRole { Name = SystemUserRole.DeviceTypeCSV },
                new IdentityRole { Name = SystemUserRole.ProductTypeOverview },
                new IdentityRole { Name = SystemUserRole.ProductTypeSiteNew },
                new IdentityRole { Name = SystemUserRole.ProductTypeEdit },
                new IdentityRole { Name = SystemUserRole.ProductTypeDelete },
                new IdentityRole { Name = SystemUserRole.ProductTypeCSV },
                new IdentityRole { Name = SystemUserRole.CSVImport },
                new IdentityRole { Name = SystemUserRole.OrderStateCSV },
                new IdentityRole { Name = SystemUserRole.OrderStatenew },
                new IdentityRole { Name = SystemUserRole.OrderStateOverview },
                new IdentityRole { Name = SystemUserRole.OrderStateEdit },
                new IdentityRole { Name = SystemUserRole.OrderStateDelete },
                new IdentityRole { Name = SystemUserRole.DeviceStateCSV },
                new IdentityRole { Name = SystemUserRole.DeviceStatenew },
                new IdentityRole { Name = SystemUserRole.DeviceStateOverview },
                new IdentityRole { Name = SystemUserRole.DeviceStateEdit },
                new IdentityRole { Name = SystemUserRole.DeviceStateDelete },
                new IdentityRole { Name = SystemUserRole.ToBeTreatedMobileDeviceOverview },
                new IdentityRole { Name = SystemUserRole.ToBeTreatedMobileDeviceNew },
                new IdentityRole { Name = SystemUserRole.ToBeTreatedMobileDeviceEdit },
                new IdentityRole { Name = SystemUserRole.ToBeTreatedMobileDeviceDelete },
                new IdentityRole { Name = SystemUserRole.ToBeTreatedMobileDeviceCSV }

            );
        }
    }
}
