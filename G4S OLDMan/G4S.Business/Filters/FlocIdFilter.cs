using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class FlocIdFilter : EntityFilterBase<FlocId>, IEntityFilter<FlocId>
    {
        public override Task<IQueryable<FlocId>> FilterAsync(IQueryable<FlocId> query, SearchBase<FlocId> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(FlocIdSearchCriteria))) {

                FlocIdSearchCriteria criteria = (FlocIdSearchCriteria)searchCriteria;

                if (criteria.FlocIdNumber.HasValue)
                {
                    query = query.Where(x => x.FlocIdNumber == criteria.FlocIdNumber.Value);
                }

                if (criteria.LoginSiteId.HasValue)
                {
                    query = query.Where(x => x.LoginSiteId == criteria.LoginSiteId.Value);
                }

            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<FlocId> Order(IQueryable<FlocId> query, SearchBase<FlocId> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "loginSiteId":
                        query = descending ? query.OrderByDescending(x => x.FlocIdNumber) : query.OrderBy(x => x.FlocIdNumber);
                        break;
                    case "flocIdNumber":
                        query = descending ? query.OrderByDescending(x => x.LoginSiteId) : query.OrderBy(x => x.LoginSiteId);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
