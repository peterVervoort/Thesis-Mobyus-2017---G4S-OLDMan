using G4S.Entities.Pocos;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Data.Entity.Infrastructure;

namespace G4S.DataAccess
{
    public class EntityContext : DbContext, IEntityContext
    {
        public EntityContext() : base("G4SConnection") {
            this.Configuration.LazyLoadingEnabled = true;
        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<FlocId> FlocIds { get; set; }
        public DbSet<LoginLicence> LoginLicences { get; set; }
        public DbSet<LoginSite> LoginSites { get; set; }
        public DbSet<LwpSetting> LwpSettings { get; set; }
        public DbSet<MobileDevice> MobileDevices { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PurchaseOrder> PuchaseOrders { get; set; }
        public DbSet<StateChange> StateChanges { get; set; }
        public DbSet<DeviceStateHistory> DeviceStateChanges { get; set; }
        public DbSet<OrderItemHistory> OrderItemChanges { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<OrderStateChange> OrderStateChanges { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<UserRoleGroup> UserRoleGroups { get; set; }
        public DbSet<RepairReason> RepairReason { get; set; }
        public DbSet<ToBeTreatedMobileDevice> ToBeTreatedMobileDevice { get; set; }
        public DbSet<ToBeTreatedLwpSetting> ToBeTreatedLwpSetting { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<StateKind> StateKinds { get; set; }



        //History
        //public DbSet<Entities.HistoryPocos.User> Users_History { get; set; }
        //public DbSet<Entities.HistoryPocos.UserRole> UserRoles_History { get; set; }
        //public DbSet<Entities.HistoryPocos.FlocId> FlocIds_History { get; set; }
        //public DbSet<Entities.HistoryPocos.LoginLicence> LoginLicences_History { get; set; }
        //public DbSet<Entities.HistoryPocos.LoginSite> LoginSites_History { get; set; }
        //public DbSet<Entities.HistoryPocos.LwpSetting> LwpSettings_History { get; set; }
        //public DbSet<Entities.HistoryPocos.MobileDevice> MobileDevices_History { get; set; }
        //public DbSet<Entities.HistoryPocos.OrderItem> OrderItems_History { get; set; }
        //public DbSet<Entities.HistoryPocos.PuchaseOrder> PuchaseOrders_History { get; set; }
        //public DbSet<Entities.HistoryPocos.SimCard> SimCards_History { get; set; }
        //public DbSet<Entities.HistoryPocos.RepairStateChange> RepairStateChanges_History { get; set; }
        //public DbSet<Entities.HistoryPocos.DeviceRepairChange> DeviceRepairChanges_History { get; set; }
        //public DbSet<Entities.HistoryPocos.OrderItemChange> OrderItemChanges_History { get; set; }
        //public DbSet<Entities.HistoryPocos.State> States_History { get; set; }
        //public DbSet<Entities.HistoryPocos.OrderStateChange> OrderStateChanges_History { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //MobileDevice <-> LwpSetting
            modelBuilder.Entity<LwpSetting>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<MobileDevice>()
                .HasRequired(md => md.LwpSetting)
                .WithRequiredPrincipal(lwp => lwp.MobileDevice);

            //ToBeTreatedMobileDevice <->ToBeTreatedLwpSetting
            modelBuilder.Entity<ToBeTreatedLwpSetting>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<ToBeTreatedMobileDevice>()
                .HasRequired(md => md.LwpSetting)
                .WithRequiredPrincipal(lwp => lwp.MobileDevice);
        }

    }
}
