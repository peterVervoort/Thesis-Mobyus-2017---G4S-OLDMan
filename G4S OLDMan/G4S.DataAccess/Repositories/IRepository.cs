using System.Collections.Generic;
using System.Threading.Tasks;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Linq.Expressions;
using System;
using G4S.Entities.Enums;

namespace G4S.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        IEnumerable<string> GetIncludeList();

        Task<TEntity> GetAsync(int id, IEnumerable<string> includes = null, DeleteOption includeDeleted = DeleteOption.NotDeleted, bool NoTracking = false);
        Task<IList<TEntity>> GetAllAsync(IEnumerable<string> includes = null, DeleteOption includeDeleted = DeleteOption.NotDeleted);


        Task<IList<TEntity>> Search(System.Linq.Expressions.Expression<Func<TEntity, bool>> searchExpression, IEnumerable<string> includes = null, DeleteOption includeDeleted = DeleteOption.NotDeleted);
        Task<IList<TEntity>> Search(SearchBase<TEntity> searchCriteria, DeleteOption includeDeleted = DeleteOption.NotDeleted, IEnumerable<string> includes = null);
        Task<int> SearchCount(SearchBase<TEntity> searchCriteria, DeleteOption includeDeleted = DeleteOption.NotDeleted);

        Task<bool> Any(System.Linq.Expressions.Expression<Func<TEntity, bool>> anyExpression, DeleteOption includeDeleted = DeleteOption.NotDeleted);
        Task<TEntity> FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> fodExpression, DeleteOption includeDeleted = DeleteOption.NotDeleted);


        TEntity Create(TEntity entity, string userName = null);

        Task<TEntity> UpdateAsync(TEntity entity, string userName = null);

        Task DeleteAsync(int id, string UserName = null);

        Task<TEntity> RestoreAsync(int id, string UserName = null);

    }
}