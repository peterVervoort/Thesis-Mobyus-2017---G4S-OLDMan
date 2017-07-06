using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class StateChangeFilter : EntityFilterBase<StateChange>, IEntityFilter<StateChange>
    {
        public override Task<IQueryable<StateChange>> FilterAsync(IQueryable<StateChange> query, SearchBase<StateChange> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(StateChangeSearchCriteria))) {

                StateChangeSearchCriteria criteria = (StateChangeSearchCriteria)searchCriteria;

                if (criteria.StateFromId.HasValue)
                {
                    query = query.Where(x => x.StateFromId == criteria.StateFromId.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.StateFrom))
                {
                    query = query.Where(x => x.StateFrom.Name.Contains(criteria.StateFrom));
                }

                if (criteria.StateToId.HasValue)
                {
                    query = query.Where(x => x.StateToId == criteria.StateToId.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.StateTo))
                {
                    query = query.Where(x => x.StateTo.Name.Contains(criteria.StateTo));
                }

                if (criteria.SystemStateChange.HasValue)
                {
                    query = query.Where(x => x.SystemStateChange == criteria.SystemStateChange.Value);
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<StateChange> Order(IQueryable<StateChange> query, SearchBase<StateChange> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "stateFrom":
                        query = descending ? query.OrderByDescending(x => x.StateFrom.Name) : query.OrderBy(x => x.StateFrom.Name);
                        break;
                    case "stateTo":
                        query = descending ? query.OrderByDescending(x => x.StateTo.Name) : query.OrderBy(x => x.StateTo.Name);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
