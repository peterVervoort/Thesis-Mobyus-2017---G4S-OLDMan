using System.Collections.Generic;
using System.Threading.Tasks;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System;
using System.Linq.Expressions;
using G4S.Entities.Enums;

namespace G4S.Business.Repositories
{
    public interface IReader<TEntity> where TEntity : EntityBase
    {
        IEnumerable<string> GetIncludeList();

        Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);
        Task<TEntity> GetById(int id, params string[] includes);
        Task<TEntity> GetByIdWithoutTracking(int id, params string[] includes);



        Task<IEnumerable<TEntity>> Search(SearchBase<TEntity> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted, params string[] includes);
        Task<int> SearchCount(SearchBase<TEntity> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted);

        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted, params string[] includes);
        Task<int> SearchCount(Expression<Func<TEntity, bool>> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted);

    }
}
 