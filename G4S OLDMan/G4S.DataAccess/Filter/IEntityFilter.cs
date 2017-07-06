using System.Linq;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.DataAccess.Repositories
{
    public interface IEntityFilter<TEntity> where TEntity : EntityBase
    {
        Task<IQueryable<TEntity>> FilterAsync(IQueryable<TEntity> query, SearchBase<TEntity> criteria);
        IQueryable<TEntity> Order(IQueryable<TEntity> query, SearchBase<TEntity> criteria);
        IQueryable<TEntity> DoPaging(IQueryable<TEntity> query, SearchBase<TEntity> searchCriteria);
    }
}