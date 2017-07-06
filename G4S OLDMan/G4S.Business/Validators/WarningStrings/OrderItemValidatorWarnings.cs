using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Validators.WarningStrings
{
    public static class OrderItemValidatorWarnings
    {
        public static string GetNullAsObject() { return " is not initialized (aka null), this is not allowed";  }
        public static string GetMandatoryField() { return " is a mandatory field and may not be left blank"; }
        public static string GetEmptyNotAllowed() { return " can not be empty"; }
        public static string GetZeroNotAllowed() { return "  may not be 0"; }
        public static string GetNotValidValue() { return " is not a valid value"; }
        public static string GetNegativeNotAllowed() { return " a negative value is not allowed";  }
        public static string GetNotFoundNoDelete() { return " is not found and can't be deleted"; }
        public static string GetUpdateNoRestore() { return " needs to be restored not updated"; }
        public static string GetNotFoundNoRestore() { return " not found in deleted items can't be restored"; }
        public static string GetNotFoundNoUpdate() { return " not found and can't be updated"; }
        public static string GetRestoreNotUpdate() { return " found in deleted items must be restored not updated"; }

    }
}
