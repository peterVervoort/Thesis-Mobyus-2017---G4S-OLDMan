using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using G4S.Business.Validators;
using System.Text;

namespace G4S.Tests
{
    [TestClass]
    public class PhoneNumberValidatorTest
    {
        PhoneNumberValidator phoneValidator = new PhoneNumberValidator();
        [TestMethod]
        public void TestPhoneValidator()
        {
            //arrange
            
            string empty = "";
            string isNull = null;
            string nineChars = "123456789";
            string thirteenChars = "1234567890123";
            string elevenChars = "12345678901";
            string correctTenChars = "0493246311";
            string correctTwelveChars = "+32493246311";
            string wrongcountryCode = "+31493246311";
            string wrongcountryCode2 = "-32493246311";
            string wrongTwelveCahrs = "+3249324631a";
            string wrongTenChars = "049324631a";
            //assert
            Assert.AreEqual(true, phoneValidator.phoneValidator(correctTenChars));
            Assert.AreEqual(true, phoneValidator.phoneValidator(correctTwelveChars));
            Assert.AreEqual(false, phoneValidator.phoneValidator(thirteenChars));
            Assert.AreEqual(false, phoneValidator.phoneValidator(nineChars));
            Assert.AreEqual(false, phoneValidator.phoneValidator(elevenChars));
            Assert.AreEqual(false, phoneValidator.phoneValidator(isNull));
            Assert.AreEqual(false, phoneValidator.phoneValidator(empty));
            Assert.AreEqual(false, phoneValidator.phoneValidator(wrongcountryCode));
            Assert.AreEqual(false, phoneValidator.phoneValidator(wrongcountryCode2));
            Assert.AreEqual(false, phoneValidator.phoneValidator(wrongTenChars));
            Assert.AreEqual(false, phoneValidator.phoneValidator(wrongTwelveCahrs));
        }

        [TestMethod]
        public void TestmultiplePhoneNumSeparatedByCommaValidator()
        {
            //arrange
            string empty = "";
            string isNull = null;
            string correctTenChars = "0493246311";
            string correctTwelveChars = "+32493246311";
            StringBuilder correctMultipleTenChars = new StringBuilder();
            string separatorEnding;
            for (int i = 0; i < 4; i++)
            {
                correctMultipleTenChars.Append(correctTenChars);
                correctMultipleTenChars.Append(",");
            }
            separatorEnding = correctMultipleTenChars.ToString();
            correctMultipleTenChars.Length--;
            StringBuilder correctMultipleTwelveChars = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                correctMultipleTwelveChars.Append(correctTwelveChars);
                correctMultipleTwelveChars.Append(",");
            }
            correctMultipleTwelveChars.Length--;
            StringBuilder correctMultipleMixedChars = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                correctMultipleMixedChars.Append(correctTenChars);
                correctMultipleMixedChars.Append(",");
                correctMultipleMixedChars.Append(correctTwelveChars);
                correctMultipleMixedChars.Append(",");
            }
            correctMultipleMixedChars.Length--;

            StringBuilder wrongSeparator = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                wrongSeparator.Append(correctTenChars);
                wrongSeparator.Append(";");
            }
            wrongSeparator.Length--;

            //assert
            Assert.AreEqual(true, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(correctTenChars));
            Assert.AreEqual(true, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(correctTwelveChars));
            Assert.AreEqual(true, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(correctMultipleMixedChars.ToString()));
            Assert.AreEqual(true, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(correctMultipleTenChars.ToString()));
            Assert.AreEqual(true, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(correctMultipleTwelveChars.ToString()));
            Assert.AreEqual(false, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(wrongSeparator.ToString()));
            Assert.AreEqual(false, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(separatorEnding.ToString()));
            Assert.AreEqual(false, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(empty));
            Assert.AreEqual(false, phoneValidator.multiplePhoneNumSeparatedByCommaValidator(isNull));
        }
    }
}

