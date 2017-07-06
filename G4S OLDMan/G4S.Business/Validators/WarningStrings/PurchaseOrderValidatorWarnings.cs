using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Validators.WarningStrings
{
    public static class PurchaseOrderValidatorWarnings
    {
        public static string GetMandatoryField() { return " is a mandatory field and may not be left blank"; }
        public static string GetUsedNotUnique() { return " has already been used, must be unique"; }
        public static string GetRestoreNotInsert() { return "  has been found in the deleted items"; }
        public static string GetNotFoundNoDelete() { return " is not found and cant be deleted"; }
        public static string GetUpdateNoRestore() { return " alrdy exists so it must be updated not restored"; }
        public static string GetNotFoundNoRestore() { return " is not found among deleted items and can't be restored"; }
        public static string GetNotFoundNoUpdate() { return " is not found and cant be updated"; }
        public static string GetRestoreNotUpdate() { return " is found among the deleted items and must be restored first"; }
        public static string GetNullAsObject() { return " is not initialized (aka null), this is not allowed";  }
        public static string GetZeroNotAllowed() { return "  may not be 0"; }
    }
}