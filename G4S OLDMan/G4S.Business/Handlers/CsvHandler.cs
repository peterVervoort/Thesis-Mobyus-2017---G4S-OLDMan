using G4S.Business.Models;
using G4S.Business.Repositories;
using G4S.Business.Validators;
using G4S.Entities.Pocos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using G4S.Business.Writers;
using G4S.Business.Helpers;
using AutoMapper;
using Microsoft.Practices.Unity;

namespace G4S.Business.Handlers
{
    internal class CsvHandler : ICsvHandler
    {
        private LwpSetting setting;

        [Dependency]
        public IWriter<LoginSite> LoginSiteWriter { get; set; }


        [Dependency]
        public IValidator<MobileDevice> MobileDeviceValidator { get; set; }
        [Dependency]
        public IReader<MobileDevice> MobileDeviceReader { get; set; }
        [Dependency]
        public IWriter<MobileDevice> MobileDeviceWriter { get; set; }


        [Dependency]
        public IWriter<LwpSetting> LwpSettingWriter { get; set; }
        [Dependency]
        public IValidator<LwpSetting> LwpSettingValidator { get; set; }

        [Dependency]
        public IWriter<ToBeTreatedMobileDevice> ToBeTreatedMobileDeviceWriter { get; set; }
        [Dependency]
        public IWriter<ToBeTreatedLwpSetting> ToBeTreatedLwpSettingWriter { get; set; }
        [Dependency]
        public IReader<ToBeTreatedMobileDevice> ToBeTreatedMobileDeviceReader { get; set; }
        [Dependency]
        public IReader<ToBeTreatedLwpSetting> ToBeTreatedLwpSettingReader { get; set; }




        public async Task HandleImportRecords(IEnumerable<CsvImportModel> importModels)
        {
            IEnumerable<ConvertedModel> convertedModels = Mapper.Map<IEnumerable<ConvertedModel>>(importModels);

            await CreateNewLoginSiteIfNeeded(convertedModels);

            foreach (ConvertedModel model in convertedModels)
            {
                ValidationResult mobileDeviceValidationResult = await ValidateMobileDevice(model.MobileDevice);
                ValidationResult lwpValidatonResult = await LwpSettingValidator.ValidateAsync(model.LwpSetting);

                if (lwpValidatonResult.Result == ValidationResultCode.Valid && mobileDeviceValidationResult.Result == ValidationResultCode.Valid)
                {
                    if ((await MobileDeviceReader.SearchCount(md => md.Reference == model.MobileDevice.Reference)) >= 1) await UpdateDevice(model.MobileDevice, model.LwpSetting);
                    else await insertDevice(model.MobileDevice, model.LwpSetting);
                }
                else
                {
                    await CreateToBeTreated(model, mobileDeviceValidationResult, lwpValidatonResult);
                }

            }

        }

        public async Task HandleToBeTreated(int id)
        {
            var tbtSetting = await ToBeTreatedLwpSettingReader.GetByIdWithoutTracking(id);
            var setting = Mapper.Map<LwpSetting>(tbtSetting);
            var tbtDevice = await ToBeTreatedMobileDeviceReader.GetByIdWithoutTracking(id);
            var device = Mapper.Map<MobileDevice>(tbtDevice);

            ValidationResult mobileDeviceValidationResult = await ValidateMobileDevice(device);
            ValidationResult lwpValidatonResult = await LwpSettingValidator.ValidateAsync(setting);

            if (lwpValidatonResult.Result == ValidationResultCode.Valid && mobileDeviceValidationResult.Result == ValidationResultCode.Valid)
            {
                if ((await MobileDeviceReader.SearchCount(md => md.Reference == device.Reference)) >= 1) await UpdateDevice(device, setting);
                else
                {
                    device.Id = 0;
                    device.Platform = null;
                    device.LoginSite = null;
                    device.OrderItem = null;
                    device.Type = null;
                    device.LwpSetting = null;
                    setting.Id = 0;
                    setting.MobileDevice = null;
                    await insertDevice(device, setting);
                }
            } else
            {
                await UpdateToBeTreated(tbtDevice, tbtSetting, mobileDeviceValidationResult, lwpValidatonResult);
            }
        }

        private async Task CreateNewLoginSiteIfNeeded(IEnumerable<ConvertedModel> convertedModels)
        {
            //Fix new loginsite
            IEnumerable<string> loginSitesToCreate = convertedModels
                                                                .Where(c => !c.MobileDevice.LoginSiteId.HasValue)
                                                                .Select(c => c.Original.Site.Trim().ToLower())
                                                                .Distinct();
            foreach (string loginSite in loginSitesToCreate)
            {
                var site = loginSite.First().ToString().ToUpper() + string.Join("", loginSite.Skip(1));
                var result = await LoginSiteWriter.InsertAsync(new LoginSite { SiteName = site });
                if (result.Code == ResultCode.Success)
                {
                    foreach (var model in convertedModels.Where(c => !c.MobileDevice.LoginSiteId.HasValue && c.Original.Site.ToLower() == loginSite))
                    {
                        model.MobileDevice.LoginSiteId = result.Entity.Id;
                    }
                }
            }
        }

        private async Task<ValidationResult> ValidateMobileDevice(MobileDevice device)
        {
            ValidationResult mobileDeviceValidationResult;
            int count = await MobileDeviceReader.SearchCount(md => md.Reference == device.Reference);
            if (count == 1)
            {
                mobileDeviceValidationResult = await MobileDeviceValidator.ValidateUpdateAsync(device);
            }
            else if (count > 1)
            {
                mobileDeviceValidationResult = new ValidationResult() { Result = ValidationResultCode.Invalid };
            }
            else
            {
                mobileDeviceValidationResult = await MobileDeviceValidator.ValidateInsertAsync(device);
            }
            return mobileDeviceValidationResult;
        }

        private async Task CreateToBeTreated(ConvertedModel model, ValidationResult mobileDeviceValidationResult, ValidationResult lwpValidatonResult)
        {
            var tbtDevice = Mapper.Map<ToBeTreatedMobileDevice>(model.MobileDevice);
            var tbtLwpSetting = Mapper.Map<ToBeTreatedLwpSetting>(model.LwpSetting);
            tbtDevice.LoginSiteOriginal = model.Original.Site;
            tbtDevice.PlatformOriginal = model.Original.Plateform;
            tbtDevice.DeviceTypeOriginal = model.Original.ControllerType;
            tbtDevice.Warnings = new Collection<ValidationWarning>();

            foreach (var message in mobileDeviceValidationResult.Messages)
            {
                tbtDevice.Warnings.Add(new ValidationWarning { Warning = message });
            }

            EntityResult<ToBeTreatedMobileDevice> result;

            var existingDevice = (await ToBeTreatedMobileDeviceReader.Search(t => t.Reference == tbtDevice.Reference)).FirstOrDefault();
            if (existingDevice != null)
            {
                tbtDevice.Id = existingDevice.Id;
                result = await ToBeTreatedMobileDeviceWriter.UpdateAsync(tbtDevice);
            }
            else
            {
                result = await ToBeTreatedMobileDeviceWriter.InsertAsync(tbtDevice);
            }

            if (result.Code == ResultCode.Success)
            {
                //insert lwpsetting
                tbtLwpSetting.Id = result.Entity.Id;
                tbtLwpSetting.Warnings = new Collection<ValidationWarning>();

                foreach (var message in lwpValidatonResult.Messages)
                {
                    tbtLwpSetting.Warnings.Add(new ValidationWarning { Warning = message });
                }
                var lwpResult = await ToBeTreatedLwpSettingWriter.InsertAsync(tbtLwpSetting);
                if (lwpResult.Code != ResultCode.Success) await MobileDeviceWriter.DeleteAsync(result.Entity.Id);
            }
        }

        private async Task UpdateToBeTreated(ToBeTreatedMobileDevice device, ToBeTreatedLwpSetting setting, 
            ValidationResult mobileDeviceValidationResult, ValidationResult lwpValidatonResult)
        {
            var result = await ToBeTreatedMobileDeviceWriter.UpdateAsync(device);
            if (result.Code == ResultCode.Success)
            {
                var lwpResult = await ToBeTreatedLwpSettingWriter.UpdateAsync(setting);
                if (lwpResult.Code != ResultCode.Success) await MobileDeviceWriter.DeleteAsync(result.Entity.Id);
            }

        }

        private async Task insertDevice(MobileDevice mobileDevice, LwpSetting lwpSetting)
        {
            var result = await MobileDeviceWriter.InsertAsync(mobileDevice);
            if (result.Code == ResultCode.Success)
            {
                //insert lwpsetting
                lwpSetting.Id = result.Entity.Id;
                var lwpResult = await LwpSettingWriter.InsertAsync(lwpSetting);
                if (lwpResult.Code == ResultCode.Success) await RemoveTobeTreated(result.Entity.Reference);
                else await MobileDeviceWriter.DeleteAsync(result.Entity.Id);
            }
        }

        private async Task UpdateDevice(MobileDevice mobileDevice, LwpSetting lwpSetting)
        {
            var devices = await MobileDeviceReader.Search(md => md.Reference == mobileDevice.Reference);
            if (!devices.Any()) throw new Exception("Unexpected condition");
            mobileDevice.Id = devices.First().Id;
            var result = await MobileDeviceWriter.UpdateAsync(mobileDevice);
            if (result.Code == ResultCode.Success)
            {
                //update lwpsetting
                lwpSetting.Id = devices.First().Id;
                var lwpResult = await LwpSettingWriter.UpdateAsync(lwpSetting);
                if (lwpResult.Code == ResultCode.Success) await RemoveTobeTreated(result.Entity.Reference);
                else await MobileDeviceWriter.DeleteAsync(result.Entity.Id);
            }
        }

        private async Task RemoveTobeTreated(string Reference)
        {
            var results = await ToBeTreatedMobileDeviceReader.Search(tbtmd => tbtmd.Reference == Reference);
            if (results.Any())
            {
                foreach (var result in results)
                {
                    await ToBeTreatedMobileDeviceWriter.DeleteAsync(result.Id);
                    await ToBeTreatedLwpSettingWriter.DeleteAsync(result.Id);
                }
            }
        }


    }

}

