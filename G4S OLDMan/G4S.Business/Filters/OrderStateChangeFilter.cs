using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class OrderStateChangeFilter : EntityFilterBase<OrderStateChange>, IEntityFilter<OrderStateChange>
    {
        public override Task<IQueryable<OrderStateChange>> FilterAsync(IQueryable<OrderStateChange> query, SearchBase<OrderStateChange> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(OrderStateChangeSearchCriteria))) {

                OrderStateChangeSearchCriteria criteria = (OrderStateChangeSearchCriteria)searchCriteria;

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

                if (criteria.ProductTypeId.HasValue)
                {
                    query = query.Where(x => x.ProductTypeId == criteria.ProductTypeId.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.ProductType))
                {
                    query = query.Where(x => x.ProductType.TypeName.Contains(criteria.ProductType));
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<OrderStateChange> Order(IQueryable<OrderStateChange> query, SearchBase<OrderStateChange> searchCriteria)
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
                    case "productType":
                        query = descending ? query.OrderByDescending(x => x.ProductType.TypeName) : query.OrderBy(x => x.ProductType.TypeName);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
