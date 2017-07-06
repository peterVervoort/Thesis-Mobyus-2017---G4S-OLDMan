using G4S.Business.Repositories;
using G4S.Business.Validators.WarningStrings;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    class UserRoleValidator : ValidatorBase<UserRole> , IValidator<UserRole>
    {
        private readonly IReader<UserRole> _reader;

        public UserRoleValidator(IReader<UserRole> reader)
        {
            _reader = reader;
        }

        public override Task<ValidationResult> ValidateDeleteAsync(UserRole entity)
        {
            ValidationResult result = new ValidationResult(ValidationResultCode.Valid);

            if (entity.SystemRole)
            {
                result.AddMessageAndSetInvalid( UserRoleValidatorWarnings.GetNotRemoveSysRole());
            }

            return Task.FromResult(result);
        }
    }
}
