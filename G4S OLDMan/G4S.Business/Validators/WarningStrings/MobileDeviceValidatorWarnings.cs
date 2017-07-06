using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Validators.WarningStrings
{
    public static class MobileDeviceWarnings
    {
        public static string GetMandatoryField() { return " is a mandatory field and may not be left blank"; }
        public static string GetValueNotValid() { return " is not a valid value"; }
        public static string GetPhoneNumberNotValid() { return " contains a not a valid phone number"; }
        public static string GetUpdateNotInsert() { return " has already been used and must be updated not inserted"; }
        public static string GetRestoreNotInsert() { return " has been found in the deleted items and must be restored not updated"; }
        public static string GetNotFoundNoDelete() { return " is not found and cant be deleted"; }
        public static string GetNotFoundNoUpdate() { return " is not found and cant be updated"; }
        public static string GetRestoreNotUpdate() { return " is found among the deleted items and must be restored first"; }
        public static string GetUpdateNotRestore() { return " alrdy exists so it must be updated not restored"; }
        public static string GetNotFoundNoRestore() { return " is not found among deleted items and can't be restored"; }
        public static string GetLwpActiveNoPhoneNumber() { return " has LWP alarm activated but contains no phone numbers to call"; }
        public static string GetMovementActiveNoTime() { return " has Movement alarm activated but contains no time before movemend alarm"; }
        public static string GetManDownActiveNoAngle() { return " has ManDown alarm activated but contains no Angle for ManDown alarm"; }
        public static string GetManDownActiveNoTime() { return " has ManDown alarm activated but contains no time before ManDown alarm"; }
        public static string GetTimerActiveNoTime() { return " has Timer alarm activated but contains no time before Timer alarm"; }
        public static string GetExternalRecieverActiveNoUniqueNumber() { return " has External Alarm Reciever activated but contains no unique identifier for this reciever"; }
        public static string GetNullNotAllowed() { return " is not initialized (aka null), this is not allowed"; }
    }

}