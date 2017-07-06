using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    public class PhoneNumberValidator : IPhoneNumberValidator
    {
        public bool phoneValidator(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return false;
            }
            if (phoneNumber.Length == 12)
            {
                string countrycode = phoneNumber.Substring(0, 3);
                string restOfNumber = phoneNumber.Substring(3, 9);
                if (!countrycode.Equals("+32"))
                {
                    return false;
                }
                int restOfNumber11Int;
                if (!int.TryParse(restOfNumber, out restOfNumber11Int))
                {
                    return false;
                }
            }
            else if (phoneNumber.Length == 10)
            {
                if (!(phoneNumber[0] == '0'))
                {
                    return false;
                }
                int restOfNumber9Int;
                if (!int.TryParse(phoneNumber, out restOfNumber9Int))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool multiplePhoneNumSeparatedByCommaValidator(string phoneNumbers)
        {
            if (phoneNumbers == null)
            {
                return false;
            }
            var numbers = phoneNumbers.Split(',');
            if (numbers.Count() == 0)
            {
                return false;
            }
            foreach (var number in numbers)
            {
                if (!phoneValidator(number))
                {
                    return false;
                } 
            }
            return true;
        }
    }
}