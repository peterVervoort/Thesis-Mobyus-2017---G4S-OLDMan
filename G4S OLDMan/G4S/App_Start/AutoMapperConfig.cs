using AutoMapper;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using G4S.Models;
using G4S.Models.LwpDevice;
using Microsoft.Practices.Unity;
using System.Linq;

namespace G4S
{
    public class AutoMapperConfig
    {
        public static void Configure(IUnityContainer container)
        {
            EntitiesToModels();
            ModelsToEntities();
            SearchModelMappings();

            //Business link
            Business.AutoMapperConfig.Configure(container);
        }

        private static void SearchModelMappings()
        {
            //users
            Mapper.CreateMap<UserSearchModel, UserSearchCriteria>();
            Mapper.CreateMap<UserRoleSearchModel, UserRoleSearchCriteria>();
            Mapper.CreateMap<UserRoleGroupSearchModel, UserRoleGroupSearchCriteria>();
            //devices
            Mapper.CreateMap<MobileDeviceSearchModel, MobileDeviceSearchCriteria>();
            //PO
            Mapper.CreateMap<PurchaseOrderSearchModel, PurchaseOrderSearchCriteria>();
            Mapper.CreateMap<OrderItemSearchModel, OrderItemSearchCriteria>();
            //translations
            Mapper.CreateMap<TranslationSearchModel, TranslationSearchCriteria>();
            Mapper.CreateMap<LanguageSearchModel, LanguageSearchCriteria>();
            //list
            Mapper.CreateMap<LwpSettingSearchModel, LwpSettingSearchCriteria>();
            Mapper.CreateMap<ToBeTreatedLwpSettingSearchModel, ToBeTreatedLwpSettingSearchCriteria>();
            Mapper.CreateMap<FlocIdSearchModel, FlocIdSearchCriteria>();
            Mapper.CreateMap<PlatformSearchModel, PlatformSearchCriteria>().
                ForMember(target => target.PlatformName, opt => opt.MapFrom(source => source.Platform));
            Mapper.CreateMap<RepairReasonSearchModel, RepairReasonSearchCriteria>();
            Mapper.CreateMap<LoginSiteSearchModel, LoginSiteSearchCriteria>();
            Mapper.CreateMap<LoginLicenceSearchModel, LoginLicenceSearchCriteria>();
            Mapper.CreateMap<DeviceTypeSearchModel, DeviceTypeSearchCriteria>();
            Mapper.CreateMap<ProductTypeSearchModel, ProductTypeSearchCriteria>();
            //flow
            Mapper.CreateMap<StateSearchModel, StateSearchCriteria>();
            Mapper.CreateMap<StateChangeSearchModel, StateChangeSearchCriteria>();
            Mapper.CreateMap<OrderStateChangeSearchModel, OrderStateChangeSearchCriteria>();
            Mapper.CreateMap<DeviceStateHistorySearchModel, DeviceStateHistorySearchCriteria>();
            Mapper.CreateMap<OrderItemHistorySearchModel, OrderItemHistorySearchCriteria>();
            Mapper.CreateMap<ToBeTreatedMobileDeviceSearchModel, ToBeTreatedMobileDeviceSearchCriteria>();

        }

        public static void EntitiesToModels()
        {
            //Base
            Mapper.CreateMap(typeof(EntityBase<>), typeof(ModelBase<>));

            Mapper.CreateMap<Language, LanguageModel>();
            Mapper.CreateMap<MobileDevice, LwpDeviceModel>()
                .ForMember(target => target.LwpSetting, opt => opt.MapFrom(source => source.LwpSetting))
                .ForMember(target => target.MobileDevice, opt => opt.MapFrom(source => Mapper.Map<MobileDevice>(source)));
            Mapper.CreateMap<FlocId, FlocIdModel>()
                .ForMember(target => target.LoginSite, opt => opt.MapFrom(source => source.LoginSite.SiteName));
            Mapper.CreateMap<LoginSite, LoginSiteModel>();
            Mapper.CreateMap<LoginLicence, LoginLicenceModel>()
                .ForMember(target => target.LoginSite, opt => opt.MapFrom(source => source.LoginSite == null ? "" : source.LoginSite.SiteName))
                .ForMember(target => target.Platform, opt => opt.MapFrom(source => source.Platform == null ? "" : source.Platform.PlatformName))
                .ForMember(target => target.PurchaseOrderNumber, opt => opt.MapFrom(source => source.OrderItem == null ? default(long?) : source.OrderItem.PurchaseOrder == null ? default(long?) : source.OrderItem.PurchaseOrder.PurchaseOrderNumber))
                .ForMember(target => target.FlocIdCount, opt => opt.MapFrom(source => source.FlocIds == null ? 0 : source.FlocIds.Count()));
            Mapper.CreateMap<User, UserModel>()
                .ForMember(target => target.Language, opt => opt.MapFrom(source => source.Language == null ? null : source.Language.Taal))
                .ForMember(target => target.RoleGroup, opt => opt.MapFrom(source => source.RoleGroup == null ? null : source.RoleGroup.Name));
            Mapper.CreateMap<Translation, TranslationModel>()
                .ForMember(target => target.Language, opt => opt.MapFrom(source => source.Language == null ? null : source.Language.Taal));
            Mapper.CreateMap<UserRole, UserRoleModel>();
            Mapper.CreateMap<UserRoleGroup, UserRoleGroupModel>();
            Mapper.CreateMap<StateKind, StateKindModel>();
            Mapper.CreateMap<State, StateModel>()
                .ForMember(target => target.Kind, opt => opt.MapFrom(source => source.Kind != null ? source.Kind.Name : ""));
            Mapper.CreateMap<DeviceStateHistory, DeviceStateHistoryModel>()
                .AfterMap((source, target) =>
                {
                    target.StateFrom = source.RepairStateChange?.StateFrom?.Name;
                    target.StateTo = source.RepairStateChange?.StateTo?.Name;
                    target.StateToColorHex = source.RepairStateChange?.StateTo?.ColorHex ?? "#d26363";
                    if (source.ChangedBy != null) target.ChangedByUser = $"{source.ChangedBy.FirstName} {source.ChangedBy.Name}";
                });
            Mapper.CreateMap<OrderItemHistory, OrderItemHistoryModel>()
                .AfterMap((source, target) =>
                {
                    target.StateFrom = source.StateChange?.StateFrom?.Name;
                    target.StateTo = source.StateChange?.StateTo?.Name;
                    target.StateToColorHex = source.StateChange?.StateTo?.ColorHex ?? "#d26363";
                    if (source.ChangedBy != null) target.ChangedByUser = $"{source.ChangedBy.FirstName} {source.ChangedBy.Name}";
                });
            Mapper.CreateMap<LwpSetting, LwpSettingModel>();
            Mapper.CreateMap<ToBeTreatedLwpSetting, ToBeTreatedLwpSettingModel>()
                .ForMember(target => target.ValidationMessages, opt => opt.MapFrom(source => source.Warnings == null ? null : source.Warnings.Where(w => !w.SoftDelete).Select(w => w.Warning)));
            
            Mapper.CreateMap<RepairReason, RepairReasonModel>()
                .ForMember(target => target.State, opt => opt.MapFrom(source => source.State.Name));
            Mapper.CreateMap<DeviceType, DeviceTypeModel>();
            Mapper.CreateMap<ProductType, ProductTypeModel>();
            Mapper.CreateMap<PurchaseOrder, PurchaseOrderModel>();
            Mapper.CreateMap<OrderItem, OrderItemModel>()
                .ForMember(target => target.PurchaseOrderNumber, opt => opt.MapFrom(source => source.PurchaseOrder.PurchaseOrderNumber))
                .ForMember(target => target.Type, opt => opt.MapFrom(source => source.Type.TypeName))
                .ForMember(target => target.DeviceType, opt => opt.MapFrom(source => source.DeviceType.TypeName))
                .AfterMap((source, target) =>
                {
                    var lastState = source.ItemChanges?.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault();
                    if (lastState != null)
                    {
                        target.LastStateDate = lastState.ChangeDate;
                        target.CurrentState = lastState.StateChange?.StateTo?.Name;
                    }
                });
            Mapper.CreateMap<Platform, PlatformModel>().
                ForMember(target => target.Platform, opt => opt.MapFrom(source => source.PlatformName));
            Mapper.CreateMap<StateChange, StateChangeModel>()
                .ForMember(target => target.StateFrom, opt => opt.MapFrom(source => source.StateFrom.Name))
                .ForMember(target => target.StateTo, opt => opt.MapFrom(source => source.StateTo.Name));
            Mapper.CreateMap<OrderStateChange, OrderStateChangeModel>()
                .ForMember(target => target.StateFrom, opt => opt.MapFrom(source => source.StateFrom.Name))
                .ForMember(target => target.StateTo, opt => opt.MapFrom(source => source.StateTo.Name))
                .ForMember(target => target.ProductType, opt => opt.MapFrom(source => source.ProductType.TypeName));
            Mapper.CreateMap<MobileDevice, MobileDeviceModel>()
                .ForMember(target => target.LoginSite, opt => opt.MapFrom(source => source.LoginSite == null ? null: source.LoginSite.SiteName))
                .ForMember(target => target.Platform, opt => opt.MapFrom(source => source.Platform == null ? null : source.Platform.PlatformName))
                .ForMember(target => target.Type, opt => opt.MapFrom(source => source.Type == null ? null : source.Type.TypeName))
                .ForMember(target => target.LwpSettingId, opt => opt.MapFrom(source => source.Id))
                .ForMember(target => target.HasLwpSetting, opt => opt.MapFrom(source => source.Type == null ? false : source.Type.LwpSettingPossible))
                .ForMember(target => target.LinkedToOrderItem, opt => opt.MapFrom(source => source.OrderItemId.HasValue))
                .AfterMap((source, target) =>
                {
                    //PO
                    target.PurchaseOrderNumber = source.OrderItem?.PurchaseOrder?.PurchaseOrderNumber;
                    //state
                    var lastState = source.RepairChanges?.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault();
                    if (lastState != null)
                    {
                        target.LastStateDate = lastState.ChangeDate;
                        target.CurrentState = lastState.RepairStateChange?.StateTo?.Name;
                    }
                });
            Mapper.CreateMap<ToBeTreatedMobileDevice, ToBeTreatedMobileDeviceModel>()
                .ForMember(target => target.ValidationMessages, opt => opt.MapFrom(source => source.Warnings == null ? null : source.Warnings.Where(w => !w.SoftDelete).Select(w => w.Warning)))
                .ForMember(target => target.LoginSite, opt => opt.MapFrom(source => source.LoginSite == null ? null : source.LoginSite.SiteName))
                .ForMember(target => target.Platform, opt => opt.MapFrom(source => source.Platform == null ? null : source.Platform.PlatformName))
                .ForMember(target => target.Type, opt => opt.MapFrom(source => source.Type == null ? null : source.Type.TypeName))
                .ForMember(target => target.LwpSettingId, opt => opt.MapFrom(source => source.Id))
                .ForMember(target => target.HasLwpSetting, opt => opt.MapFrom(source => source.Type == null ? false : source.Type.LwpSettingPossible));
        }

        public static void ModelsToEntities()
        {
            //Base
            Mapper.CreateMap(typeof(PostModelBase<>), typeof(EntityBase<>));
            Mapper.CreateMap(typeof(SearchModelBase<>), typeof(SearchBase<>));

            Mapper.CreateMap<LanguagePostModel, Language>();
            Mapper.CreateMap<TranslationPostModel, Translation>();
            Mapper.CreateMap<FlocIdPostModel, FlocId>();
            Mapper.CreateMap<LoginSitePostModel, LoginSite>();
            Mapper.CreateMap<LoginLicencePostModel, LoginLicence>();
            Mapper.CreateMap<StatePostModel,State>();
            Mapper.CreateMap<UserRolePostModel, UserRole>();
            Mapper.CreateMap<UserRoleGroupPostModel, UserRoleGroup>();
            Mapper.CreateMap<UserPostModel, User>();
            Mapper.CreateMap<PurchaseOrderPostModel, PurchaseOrder>();
            Mapper.CreateMap<OrderItemPostModel, OrderItem>();
            Mapper.CreateMap<LwpSettingPostModel, LwpSetting>();
            Mapper.CreateMap<ToBeTreatedLwpSettingPostModel, ToBeTreatedLwpSetting>()
                .ForMember(target => target.Warnings, opt => opt.Ignore());

            Mapper.CreateMap<LwpDevicePostModel, MobileDevice>()
                .AfterMap((source, target) => {
                    target = Mapper.Map<MobileDevice>(source.MobileDevice);
                });
            Mapper.CreateMap<LwpDevicePostModel, LwpSetting>()
                .AfterMap((source, target) => {
                    target = Mapper.Map<LwpSetting>(source.LwpSetting);
                });

            Mapper.CreateMap<PlatformPostModel, Platform>().
                ForMember(target => target.PlatformName, opt => opt.MapFrom(source => source.Platform));
            Mapper.CreateMap<RepairReasonPostModel, RepairReason>();
            Mapper.CreateMap<DeviceTypePostModel, DeviceType>();
            Mapper.CreateMap<ProductTypePostModel, ProductType>();
            Mapper.CreateMap<MobileDevicePostModel, MobileDevice>();
            Mapper.CreateMap<ToBeTreatedMobileDevicePostModel, ToBeTreatedMobileDevice>()
                .ForMember(target => target.Warnings, opt => opt.Ignore());
            Mapper.CreateMap<StateChangePostModel, StateChange>();
            Mapper.CreateMap<OrderStateChangePostModel, OrderStateChange>();
            Mapper.CreateMap<DeviceStateHistoryPostModel, DeviceStateHistory>();
            Mapper.CreateMap<OrderItemHistoryPostModel, OrderItemHistory>();

        }
    }
}