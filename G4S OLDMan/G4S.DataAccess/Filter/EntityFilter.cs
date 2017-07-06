using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Linq;
using System.Threading.Tasks;

namespace G4S.DataAccess.Repositories
{
    public class EntityFilterBase<TEntity> : IEntityFilter<TEntity> where TEntity : EntityBase
    {

        public virtual async Task<IQueryable<TEntity>> FilterAsync(IQueryable<TEntity> query, SearchBase<TEntity> searchCriteria)
        {
            if (query == null) return null;

            if (searchCriteria.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchCriteria.Id.Value);
            }

            if (searchCriteria.Deleted.HasValue)
            {
                switch (searchCriteria.Deleted.Value)
                {
                    case DeleteOption.NotDeleted:
                        query = query.Where(x => !x.SoftDelete);
                        break;
                    case DeleteOption.OnlyDeleted:
                        query = query.Where(x => x.SoftDelete);
                        break;
                    case DeleteOption.Both:
                    default:
                        break;
                }
            }
            else
            {
                query = query.Where(x => !x.SoftDelete);
            }


            return query;
        }

        public virtual IQueryable<TEntity> Order(IQueryable<TEntity> query, SearchBase<TEntity> searchCriteria)
        {
            if (query == null) return null;

            if (string.IsNullOrEmpty(searchCriteria.SortField))
            {
                query = query.OrderBy(x => x.Id);
            }

            return query;
        }

        public virtual IQueryable<TEntity> DoPaging(IQueryable<TEntity> query, SearchBase<TEntity> searchCriteria)
        {
            if (query == null) return null;

            if (searchCriteria.CurrentPage.HasValue)
            {
                query = query.Skip((searchCriteria.CurrentPage.Value - 1) * searchCriteria.ItemsPerPage.Value);
            }

            if (searchCriteria.ItemsPerPage.HasValue)
            {
                query = query.Take(searchCriteria.ItemsPerPage.Value);
            }

            return query;
        }
    }
}
