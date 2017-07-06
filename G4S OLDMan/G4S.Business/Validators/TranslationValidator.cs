using G4S.Business.Repositories;
using G4S.Business.Validators.WarningStrings;
using G4S.Entities.Pocos;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    class TranslationValidator : ValidatorBase<Translation> , IValidator<Translation>
    {
        private readonly IReader<Translation> _reader;

        public TranslationValidator(IReader<Translation> reader)
        {
            _reader = reader;
        }

        public override async Task<ValidationResult> ValidateInsertAsync(Translation entity)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);

            var count =  await _reader.SearchCount(t => t.LanguageId == entity.LanguageId
                                                     && t.Group == entity.Group
                                                     && t.Keyword == entity.Keyword);

            if (count > 0)
            {
                result.AddMessageAndSetInvalid($"{entity.Group}.{entity.Keyword}" + TranslationWarnings.GetDoubleInsert());
            }

            return result;
        }
        //TODO Validate Delete Restore Update
    }
}
