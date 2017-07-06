using G4S.Business.Repositories;
using G4S.Business.Validators.WarningStrings;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace G4S.Business.Validators
{
    class MobileDeviceValidator : ValidatorBase<MobileDevice> , IValidator<MobileDevice>
    {
        private readonly IReader<MobileDevice> _readerMobileDevice;
        private readonly IReader<DeviceType> _readerDeviceType;
        private readonly IReader<Platform> _readerPlatform;
        private readonly IReader<LoginSite> _loginSiteReader;
        private PhoneNumberValidator phoneValidator = new PhoneNumberValidator();

        private readonly IValidator<LwpSetting> _lwpSettingValidator;
       

        public MobileDeviceValidator(IReader<MobileDevice> readerMobileDevice, IReader<DeviceType> readerDeviceType, IReader<Platform> readerPlatform, IReader<LoginSite> loginSiteReader, IValidator<LwpSetting> lwpSettingValidator)
        {
            _readerMobileDevice = readerMobileDevice;
            _readerDeviceType = readerDeviceType;
            _lwpSettingValidator = lwpSettingValidator;
            _readerPlatform = readerPlatform;
            _loginSiteReader = loginSiteReader;
        }
        public override async Task<ValidationResult> ValidateAsync(MobileDevice mobileDevice)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (mobileDevice == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(MobileDevice)}" + MobileDeviceWarnings.GetNullNotAllowed());
            }
            else
            {
                if (string.IsNullOrWhiteSpace(mobileDevice.Reference))
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}" + MobileDeviceWarnings.GetMandatoryField());
                }

                if (!mobileDevice.DeviceTypeId.HasValue)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Type)}" + MobileDeviceWarnings.GetMandatoryField());
                }
                if (mobileDevice.DeviceTypeId == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Type)}" + MobileDeviceWarnings.GetNullNotAllowed());
                }
                else
                {
                    if (!(await _readerDeviceType.Search(d => d.Id == mobileDevice.DeviceTypeId)).Any())
                    {
                        result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Type)}: {mobileDevice.Type}" + MobileDeviceWarnings.GetValueNotValid());
                    }
                }

                //TODO if state of Device != spare || Delivered || Shipped to TrackForce
                if (string.IsNullOrWhiteSpace(mobileDevice.PhoneNumber))
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.PhoneNumber)}" + MobileDeviceWarnings.GetMandatoryField());
                }

                if (!phoneValidator.phoneValidator(mobileDevice.PhoneNumber))
                {
                    result.AddMessageAndSetInvalid($"{mobileDevice.PhoneNumber}" + MobileDeviceWarnings.GetPhoneNumberNotValid());
                }     

                if (mobileDevice.PlatformId == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Platform)}" + MobileDeviceWarnings.GetNullNotAllowed());
                }
                else
                {
                    if (!(await _readerPlatform.Search(d => d.Id == mobileDevice.PlatformId)).Any())
                    {
                        result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Platform)}" + MobileDeviceWarnings.GetValueNotValid());
                    }
                }

                if (mobileDevice.LoginSiteId == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.LoginSite)}" + MobileDeviceWarnings.GetNullNotAllowed());
                }
                else
                {
                    if (!(await _loginSiteReader.Search(d => d.Id == mobileDevice.LoginSiteId)).Any())
                    {
                        result.AddMessageAndSetInvalid($"{nameof(MobileDevice.LoginSite)}" + MobileDeviceWarnings.GetValueNotValid());
                    }
                }
            }
           

            return result;
        }
        public override async Task<ValidationResult> ValidateInsertAsync(MobileDevice mobileDevice)
        {
            ValidationResult result = await ValidateAsync(mobileDevice);
            if (mobileDevice == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Type)}" + MobileDeviceWarnings.GetNullNotAllowed());
            }
            else
            {
                int deviceCount = await _readerMobileDevice.SearchCount(md => md.Reference == mobileDevice.Reference);
                if (deviceCount > 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}: {mobileDevice.Reference}" + MobileDeviceWarnings.GetUpdateNotInsert());
                }

                int deletedDeviceCount = await _readerMobileDevice.SearchCount(md => md.Reference == mobileDevice.Reference, DeleteOption.OnlyDeleted);
                if (deletedDeviceCount > 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}: {mobileDevice.Reference}" + MobileDeviceWarnings.GetRestoreNotInsert());
                }
            }
            return result;
        }
        public override async Task<ValidationResult> ValidateDeleteAsync(MobileDevice mobileDevice)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);
            if (mobileDevice == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Type)}" + MobileDeviceWarnings.GetNullNotAllowed());
            }
            else
            {
                int deviceCount = await _readerMobileDevice.SearchCount(md => md.Reference == mobileDevice.Reference);
                if (deviceCount == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}: {mobileDevice.Reference}" + MobileDeviceWarnings.GetNotFoundNoDelete());
                }
            }
            return result;
        }
        public override async Task<ValidationResult> ValidateUpdateAsync(MobileDevice mobileDevice)
        {
            ValidationResult result = await ValidateAsync(mobileDevice);
            if (mobileDevice == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Type)}" + MobileDeviceWarnings.GetNullNotAllowed());
            }
            else
            {
                int deviceCount = await _readerMobileDevice.SearchCount(md => md.Reference == mobileDevice.Reference);
                if (deviceCount == 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}: {mobileDevice.Reference}" + MobileDeviceWarnings.GetNotFoundNoUpdate());
                    int deletedDeviceCount = await _readerMobileDevice.SearchCount(md => md.Reference == mobileDevice.Reference, DeleteOption.OnlyDeleted);
                    if (deletedDeviceCount == 1)
                    {
                        result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}: {mobileDevice.Reference}" + MobileDeviceWarnings.GetRestoreNotUpdate());
                    }
                }
            }
            return result;
        }
        public override async Task<ValidationResult> ValidateRestoreAsync(MobileDevice mobileDevice)
        {
            ValidationResult result = await ValidateAsync(mobileDevice);
            if (mobileDevice == null)
            {
                result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Type)}" + MobileDeviceWarnings.GetNullNotAllowed());
            }
            else
            {
                int deletedDeviceCount = await _readerMobileDevice.SearchCount(md => md.Reference == mobileDevice.Reference, DeleteOption.OnlyDeleted);
                int deviceCount = await _readerMobileDevice.SearchCount(md => md.Reference == mobileDevice.Reference);
                if (deviceCount > 0)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}: {mobileDevice.Reference}" + MobileDeviceWarnings.GetUpdateNotRestore());
                }

                if (deletedDeviceCount != 1)
                {
                    result.AddMessageAndSetInvalid($"{nameof(MobileDevice.Reference)}: {mobileDevice.Reference}" + MobileDeviceWarnings.GetNotFoundNoRestore());
                }
            }
            return result;
        }

    }
}
