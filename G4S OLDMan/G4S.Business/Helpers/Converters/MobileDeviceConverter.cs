using System;
using AutoMapper;
using G4S.Business.Models;
using G4S.Entities.Pocos;
using Microsoft.Practices.Unity;
using G4S.Business.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using G4S.Business.Helpers;

namespace G4S.Business
{
    internal class MobileDeviceConverter : ITypeConverter<CsvImportModel, MobileDevice>
    {
        [Dependency]
        public IReader<DeviceType> DeviceTypeReader { get; set; }
        [Dependency]
        public IReader<Platform> PlatfromReader { get; set; }
        [Dependency]
        public IReader<LoginSite> LoginSiteReader { get; set; }

        public MobileDevice Convert(ResolutionContext context)
        {
            return Task.Run(async () => await ConvertAsync((CsvImportModel)context.SourceValue)).Result;
        }

        
        public async Task<MobileDevice> ConvertAsync(CsvImportModel import)
        {
            DeviceType deviceType = (await DeviceTypeReader.Search(dt => dt.TypeName.ToLower() == import.ControllerType.ToLower() || dt.CsvSynonyms.Contains(import.ControllerType))).FirstOrDefault();
            Platform platform = (await PlatfromReader.Search(p => p.PlatformName.ToLower() == import.Plateform.ToLower() || p.CsvSynonyms.Contains(import.Plateform))).FirstOrDefault();
            LoginSite loginSite = (await LoginSiteReader.Search(s => s.SiteName.ToLower() == import.Site.ToLower() || s.CsvSynonyms.Contains(import.Site))).FirstOrDefault();

            MobileDevice mobileDevice = new MobileDevice();
            mobileDevice.DeviceName = import.Material;
            mobileDevice.Reference = import.Reference;
            mobileDevice.DeviceTypeId = deviceType?.Id;
            mobileDevice.PhoneNumber = import.DevicePhoneNumber;
            mobileDevice.PlatformId = platform?.Id;
            mobileDevice.LoginSiteId = loginSite?.Id;

            return mobileDevice;

        }
    }
}