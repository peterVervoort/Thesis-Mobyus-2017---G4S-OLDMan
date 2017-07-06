using G4S.DataAccess.Repositories;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace G4S.Business.Repositories
{
    public class ReaderBase<TEntity> : IReader<TEntity> where TEntity : EntityBase
    {
        [Dependency]
        public IRepository<TEntity> Repository { get; set; }


        public async Task<TEntity> GetById(int id, params string[] includes)
        {
            return await Repository.GetAsync(id, includes, includeDeleted: Entities.Enums.DeleteOption.Both);
        }

        public async Task<TEntity> GetByIdWithoutTracking(int id, params string[] includes)
        {
            return await Repository.GetAsync(id, includes, includeDeleted: Entities.Enums.DeleteOption.Both, NoTracking:true);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
        {
            return await Repository.GetAllAsync(includes);
        }

        public IEnumerable<string> GetIncludeList()
        {
            return Repository.GetIncludeList();
        }

        public async Task<IEnumerable<TEntity>> Search(SearchBase<TEntity> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted, params string[] includes)
        {
            return await Repository.Search(criteriea, includeDeleted: includeDeleted, includes: includes);
        }

        public async Task<int> SearchCount(SearchBase<TEntity> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted)
        {
            return await Repository.SearchCount(criteriea, includeDeleted: includeDeleted);
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted, params string[] includes)
        {
            return await Repository.Search(criteriea, includeDeleted: includeDeleted, includes:includes);
        }

        public async Task<int> SearchCount(Expression<Func<TEntity, bool>> criteriea, DeleteOption includeDeleted = DeleteOption.NotDeleted)
        {
            return (await Search(criteriea, includeDeleted: includeDeleted)).Count();
        }
    }
}
 