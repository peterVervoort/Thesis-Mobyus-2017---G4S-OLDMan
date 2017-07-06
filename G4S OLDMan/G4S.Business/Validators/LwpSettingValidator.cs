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
    class LwpSettingValidator : ValidatorBase<LwpSetting>, IValidator<LwpSetting>
    {
        private PhoneNumberValidator phoneValidator = new PhoneNumberValidator();


        public override async Task<ValidationResult> ValidateAsync(LwpSetting lwpSetting)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);


            if (lwpSetting.TelephoneAlarmActivated == true)
            {
                if (string.IsNullOrEmpty(lwpSetting.PhoneNumbersForTelephoneAlarm))
                {
                    result.AddMessageAndSetInvalid($"{nameof(LwpSetting)}: " + MobileDeviceWarnings.GetLwpActiveNoPhoneNumber());
                }
                else if (!phoneValidator.multiplePhoneNumSeparatedByCommaValidator(lwpSetting.PhoneNumbersForTelephoneAlarm))
                {
                    result.AddMessageAndSetInvalid($"{nameof(LwpSetting)}: " + MobileDeviceWarnings.GetPhoneNumberNotValid());
                }
            }
            if (lwpSetting.MovementDetectionActivated == true)
            {
                if (lwpSetting.TimeBeforeMovementAlarmInSeconds < 1 || lwpSetting.TimeBeforeMovementAlarmInSeconds == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(LwpSetting)}: " + MobileDeviceWarnings.GetMovementActiveNoTime());
                }
            }
            if (lwpSetting.ManDownAlarmActivated == true)
            {
                if (lwpSetting.AngleOfManDownDetection < 1 || lwpSetting.AngleOfManDownDetection == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(LwpSetting)}: " + MobileDeviceWarnings.GetManDownActiveNoAngle());
                }
                if (lwpSetting.TimeBeforeManDownAlarmInSeconds < 1 || lwpSetting.TimeBeforeManDownAlarmInSeconds == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(LwpSetting)}: " + MobileDeviceWarnings.GetManDownActiveNoTime());
                }
            }
            if (lwpSetting.TimerAlarmActivated == true)
            {
                if (lwpSetting.TimeBeforeTimerAlarmInSeconds < 1 || lwpSetting.TimeBeforeTimerAlarmInSeconds == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(LwpSetting)}: " + MobileDeviceWarnings.GetTimerActiveNoTime());
                }
            }
            if (lwpSetting.SendAlarmToExternalAlarmReciverActivated == true)
            {
                if (lwpSetting.UniqueIdentifierToSendToExternalAlarmReciever == null)
                {
                    result.AddMessageAndSetInvalid($"{nameof(LwpSetting)}: " + MobileDeviceWarnings.GetExternalRecieverActiveNoUniqueNumber());
                }
            }

            return result;
        }




    }
}
