using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class StateFilter : EntityFilterBase<State>, IEntityFilter<State>
    {
        public override Task<IQueryable<State>> FilterAsync(IQueryable<State> query, SearchBase<State> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(StateSearchCriteria))) {

                StateSearchCriteria criteria = (StateSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                {
                    query = query.Where(x => x.Name.Contains(criteria.Name));
                }

                if (!string.IsNullOrWhiteSpace(criteria.Description))
                {
                    query = query.Where(x => x.Description.Contains(criteria.Description));
                }

                if (!string.IsNullOrEmpty(criteria.Tag))
                {
                    query = query.Where(x => x.Tag.Contains(criteria.Tag));
                }

                if (!string.IsNullOrEmpty(criteria.Kind))
                {
                    query = query.Where(x => x.Kind != null && x.Kind.Name.Contains(criteria.Kind));
                }

                if (criteria.IsSpare.HasValue)
                {
                    query = query.Where(x => x.IsSpare == criteria.IsSpare.Value);
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<State> Order(IQueryable<State> query, SearchBase<State> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "name":
                        query = descending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                        break;
                    case "description":
                        query = descending ? query.OrderByDescending(x => x.Description) : query.OrderBy(x => x.Description);
                        break;
                    case "tag":
                        query = descending ? query.OrderByDescending(x => x.Tag) : query.OrderBy(x => x.Tag);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
