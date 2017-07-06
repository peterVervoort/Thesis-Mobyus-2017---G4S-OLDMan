namespace G4S.Business.Validators
{
    public interface IPhoneNumberValidator
    {
        bool multiplePhoneNumSeparatedByCommaValidator(string phoneNumbers);
        bool phoneValidator(string phoneNumber);
    }
}