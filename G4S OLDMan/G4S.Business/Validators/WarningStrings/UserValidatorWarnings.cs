using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Validators.WarningStrings
{
    public static class UserValidatorWarnings
    {
        public static string GetDoubleUse() { return " has already been used"; }
        public static string GetChangeNotAllowed() { return " cannot be changed"; }
    }
}