using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class RepairReasonFilter : EntityFilterBase<RepairReason>, IEntityFilter<RepairReason>
    {
        public override Task<IQueryable<RepairReason>> FilterAsync(IQueryable<RepairReason> query, SearchBase<RepairReason> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(RepairReasonSearchCriteria))) {

                RepairReasonSearchCriteria criteria = (RepairReasonSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.Reason))
                {
                    query = query.Where(x => x.Reason.Contains(criteria.Reason));
                }

                if (!string.IsNullOrWhiteSpace(criteria.State))
                {
                    query = query.Where(x => x.State.Name.Contains(criteria.State));
                }

                if (criteria.StateId.HasValue)
                {
                    query = query.Where(x => x.StateId == criteria.StateId);
                }

            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<RepairReason> Order(IQueryable<RepairReason> query, SearchBase<RepairReason> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "reason":
                        query = descending ? query.OrderByDescending(x => x.Reason) : query.OrderBy(x => x.Reason);
                        break;
                    case "state":
                        query = descending ? query.OrderByDescending(x => x.State.Name) : query.OrderBy(x => x.State.Name);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
