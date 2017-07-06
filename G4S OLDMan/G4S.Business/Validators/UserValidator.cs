using G4S.Business.Repositories;
using G4S.Business.Validators.WarningStrings;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    class UserValidator : ValidatorBase<User> , IValidator<User>
    {
        private readonly IReader<User> _reader;

        public UserValidator(IReader<User> reader)
        {
            _reader = reader;
        }

        public override async Task<ValidationResult> ValidateAsync(User entity)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);

            int emailCount = await _reader.SearchCount(new UserSearchCriteria { Email = entity.Email });
            if (emailCount > 0)
            {
                result.AddMessageAndSetInvalid($"{nameof(User.Email)}: {entity.Email}" + UserValidatorWarnings.GetDoubleUse());
            }

            int sameNameCount = await _reader.SearchCount(new UserSearchCriteria { FirstName = entity.FirstName, Name = entity.Name });
            if (sameNameCount > 0)
            {
                result.AddMessageAndSetInvalid($"This {nameof(User)}" + UserValidatorWarnings.GetDoubleUse());
            }

            return result;
        }

        public override async Task<ValidationResult> ValidateUpdateAsync(User entity)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);

            int emailCount = await _reader.SearchCount(new UserSearchCriteria { Email = entity.Email });
            if (emailCount > 1)
            {
                result.AddMessageAndSetInvalid($"{nameof(User.Email)}: {entity.Email}" + UserValidatorWarnings.GetDoubleUse());
            }
            if (emailCount == 0)
            {
                result.AddMessageAndSetInvalid($"{nameof(User.Email)}" + UserValidatorWarnings.GetChangeNotAllowed());
            }

            return result;
        }

        public override Task<ValidationResult> ValidateDeleteAsync(User entity)
        {
            return Task.FromResult(new ValidationResult(ValidationResultCode.Valid));
        }

    }
}
