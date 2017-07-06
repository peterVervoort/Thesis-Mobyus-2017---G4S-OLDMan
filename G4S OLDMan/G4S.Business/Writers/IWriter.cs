using System.Threading.Tasks;
using G4S.Business.Helpers;
using G4S.Entities.Pocos;
using System.Collections.Generic;

namespace G4S.Business.Writers
{
    public interface IWriter<TEntity> where TEntity : EntityBase
    {
        Task<EntityResult<TEntity>> DeleteAsync(int id);
        Task<EntityResult<TEntity>> InsertAsync(TEntity entity);
        Task<EntityResult<TEntity>> UpdateAsync(TEntity entity);
        Task<EntityResult<TEntity>> RestoreAsync(int id);
    }
}