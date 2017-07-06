using AutoMapper;
using G4S.Business.Filters;
using G4S.Business.Handlers;
using G4S.Business.Models;
using G4S.Business.Readers;
using G4S.Business.Repositories;
using G4S.Business.Services;
using G4S.Business.Validators;
using G4S.Business.Writers;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;

namespace G4S.Business
{
    public class Factory
    {
        public static void Configure(IUnityContainer container)
        {
            DataAccess.Factory.Configure(container);
            Entities.Factory.Configure(container);


            //Generic base classes
            container.RegisterType(typeof(IReader<>), typeof(ReaderBase<>));
            container.RegisterType(typeof(IWriter<>), typeof(Writer<>));
            container.RegisterType(typeof(IValidator<>), typeof(ValidatorBase<>));

            //Mappers
            container.RegisterType<ITypeConverter<CsvImportModel, MobileDevice>, MobileDeviceConverter>();

            //Custom validators
            container.RegisterType<IValidator<MobileDevice>, MobileDeviceValidator>();
            container.RegisterType<IValidator<LwpSetting>, LwpSettingValidator>();
            container.RegisterType<IValidator<User>, UserValidator>();
            container.RegisterType<IValidator<Translation>, TranslationValidator>();
            container.RegisterType<IValidator<OrderItem>, OrderItemValidator>();
            container.RegisterType<IPhoneNumberValidator, PhoneNumberValidator>();
            container.RegisterType<IValidator<PurchaseOrder>, PurchaseOrderValidator>();
            container.RegisterType<IValidator<UserRole>, UserRoleValidator>();

            //Custom EntityFilters
            container.RegisterType<IEntityFilter<User>, UserFilter>();
            container.RegisterType<IEntityFilter<UserRole>, UserRoleFilter>();
            container.RegisterType<IEntityFilter<UserRoleGroup>, UserRoleGroupFilter>();
            container.RegisterType<IEntityFilter<PurchaseOrder>, PurchaseOrderFilter>();
            container.RegisterType<IEntityFilter<OrderItem>, OrderItemFilter>();
            container.RegisterType<IEntityFilter<MobileDevice>, MobileDeviceFilter>();
            container.RegisterType<IEntityFilter<ToBeTreatedMobileDevice>, ToBeTreatedMobileDeviceFilter>();
            container.RegisterType<IEntityFilter<Translation>, TranslationFilter>();
            container.RegisterType<IEntityFilter<Language>, LanguageFilter>();
            container.RegisterType<IEntityFilter<RepairReason>, RepairReasonFilter>();
            container.RegisterType<IEntityFilter<FlocId>, FlocIdFilter>();
            container.RegisterType<IEntityFilter<State>, StateFilter>();
            container.RegisterType<IEntityFilter<LwpSetting>, LwpSettingFilter>();
            container.RegisterType<IEntityFilter<ToBeTreatedLwpSetting>, ToBeTreatedLwpSettingFilter>();
            container.RegisterType<IEntityFilter<ProductType>, ProductTypeFilter>();
            container.RegisterType<IEntityFilter<LoginSite>, LoginSiteFilter>();
            container.RegisterType<IEntityFilter<LoginLicence>, LoginLicenceFilter>();
            container.RegisterType<IEntityFilter<Platform>, PlatformFilter>();
            container.RegisterType<IEntityFilter<StateChange>, StateChangeFilter>();
            container.RegisterType<IEntityFilter<OrderStateChange>, OrderStateChangeFilter>();
            container.RegisterType<IEntityFilter<DeviceType>, DeviceTypeFilter>();
            container.RegisterType<IEntityFilter<DeviceStateHistory>, DeviceStateHistoryFilter>();

            //Custom writers
            container.RegisterType<IWriter<UserRole>, UserRoleWriter>();
            container.RegisterType<IUserRoleWriter, UserRoleWriter>();
            container.RegisterType<IUserRoleGroupWriter, UserRoleGroupWriter>();
            container.RegisterType<IUserWriter, UserWriter>();
            container.RegisterType<IWriter<DeviceStateHistory>, DeviceStateHistoryWriter>();
            container.RegisterType<IWriter<OrderItemHistory>, OrderItemHistoryWriter>();
            container.RegisterType<IWriter<MobileDevice>, MobileDeviceWriter>();
            container.RegisterType<IWriter<LoginSite>, LoginSiteWriter>();
            container.RegisterType<IWriter<PurchaseOrder>, PurchaseOrderWriter>();


            //Custom Readers
            container.RegisterType<IReader<UserRole>, UserRoleReader>();
            container.RegisterType<IUserRoleReader, UserRoleReader>();

            //Services
            container.RegisterType<ICsvService, CsvService>();

            //Handlers
            container.RegisterType<IDeviceStateHistoryHandler, DeviceStateHistoryHandler>();
            container.RegisterType<IOrderItemStateHistoryHandler, OrderItemStateHistoryHandler>();
            container.RegisterType<ICsvHandler, CsvHandler>();
            container.RegisterType<IOrderItemHandler, OrderItemHandler>();
            container.RegisterType<IDeviceReplacementHandler, DeviceReplacementHandler>();
            container.RegisterType<ILwpDeviceHandler, LwpDeviceHandler>();

        }
    }
}
