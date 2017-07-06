using AutoMapper;
using G4S.Business.Models;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;
using System;

namespace G4S.Business
{
    public class AutoMapperConfig
    {
        public static void Configure(IUnityContainer container)
        {
            MapBusinessModels(container);
        }

        private static void MapBusinessModels(IUnityContainer container)
        {
            var converter = container.Resolve<ITypeConverter<CsvImportModel, MobileDevice>>();

            Mapper.CreateMap(typeof(string), typeof(bool?)).ConvertUsing<StringToNullableBoolConverter>();
            Mapper.CreateMap<CsvImportModel, MobileDevice>().ConvertUsing(converter);


            Mapper.CreateMap<CsvImportModel, LwpSetting>()
                .ForMember(target => target.TelephoneAlarmActivated, opt => opt.MapFrom(source => source.IsLWPPhones))
                .ForMember(target => target.PanicButtonActivated, opt => opt.MapFrom(source => source.IsLWPPanic))
                .ForMember(target => target.MovementDetectionActivated, opt => opt.MapFrom(source => source.IsLWPMovement))
                .ForMember(target => target.ManDownAlarmActivated, opt => opt.MapFrom(source => source.IsLWPManDown))
                .ForMember(target => target.TimerAlarmActivated, opt => opt.MapFrom(source => source.IsLWPTimer))
                .ForMember(target => target.FallAlarmActivated, opt => opt.MapFrom(source => source.IsLWPFall))
                .ForMember(target => target.SchockAlarmActivated, opt => opt.MapFrom(source => source.IsLWPImpact))

                .ForMember(target => target.TimeBeforeMovementAlarmInSeconds, opt => opt.MapFrom(source => source.IsLWPMovementTimeout))
                .ForMember(target => target.AngleOfManDownDetection, opt => opt.MapFrom(source => source.LWPManDownAngle))
                .ForMember(target => target.TimeBeforeManDownAlarmInSeconds, opt => opt.MapFrom(source => source.LWPManDownTimout))
                .ForMember(target => target.TimeBeforeTimerAlarmInSeconds, opt => opt.MapFrom(source => source.IsLWPTimerTimeout));


            Mapper.CreateMap<CsvImportModel, ConvertedModel>()
                .ForMember(target => target.MobileDevice, opt => opt.MapFrom(source => Mapper.Map<MobileDevice>(source)))
                .ForMember(target => target.LwpSetting, opt => opt.MapFrom(source => Mapper.Map<LwpSetting>(source)))
                .ForMember(target => target.Original, opt => opt.MapFrom(source => source));


            Mapper.CreateMap<MobileDevice, ToBeTreatedMobileDevice>();
            Mapper.CreateMap<LwpSetting, ToBeTreatedLwpSetting>();

            Mapper.CreateMap<MobileDevice, ToBeTreatedMobileDevice>().ReverseMap();
            Mapper.CreateMap<LwpSetting, ToBeTreatedLwpSetting>().ReverseMap();
        }
        
    }
}