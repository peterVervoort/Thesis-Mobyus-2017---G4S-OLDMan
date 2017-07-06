using System.Collections.Generic;
using G4S.Entities.Pocos;
using System.Threading.Tasks;

namespace G4S.Business.Validators
{
    public interface IValidator<Tentity> where Tentity : EntityBase
    {
        Task<ValidationResult> ValidateAsync(Tentity entity);
        Task<ValidationResult> ValidateInsertAsync(Tentity entity);
        Task<ValidationResult> ValidateDeleteAsync(Tentity entity);
        Task<ValidationResult> ValidateUpdateAsync(Tentity entity);
        Task<ValidationResult> ValidateRestoreAsync(Tentity entity);
    }
}