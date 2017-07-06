using G4S.Entities.Pocos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    public class ValidatorBase<Tentity> : IValidator<Tentity> where Tentity : EntityBase
    {
        public virtual Task<ValidationResult> ValidateAsync(Tentity entity)
        {
            return Task.FromResult(new ValidationResult(ValidationResultCode.Valid));
        }

        public virtual async Task<ValidationResult> ValidateInsertAsync(Tentity entity)
        {
            return await ValidateAsync(entity);
        }

        public virtual async Task<ValidationResult> ValidateDeleteAsync(Tentity entity)
        {
            return await ValidateAsync(entity);
        }

        public virtual async Task<ValidationResult> ValidateUpdateAsync(Tentity entity)
        {
            return await ValidateAsync(entity);
        }

        public virtual async Task<ValidationResult> ValidateRestoreAsync(Tentity entity)
        {
            return await ValidateAsync(entity);
        }
    }
}
