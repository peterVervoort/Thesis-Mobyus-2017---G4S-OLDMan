using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class LoginSiteFilter : EntityFilterBase<LoginSite>, IEntityFilter<LoginSite>
    {
        public override Task<IQueryable<LoginSite>> FilterAsync(IQueryable<LoginSite> query, SearchBase<LoginSite> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(LoginSiteSearchCriteria)))
            {

                LoginSiteSearchCriteria criteria = (LoginSiteSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.SiteName))
                {
                    query = query.Where(x => x.SiteName.Contains(criteria.SiteName));
                }

                if (criteria.UserId.HasValue)
                {
                    query = query.Where(x => x.Users.Any(u => u.Id == criteria.UserId.Value));
                }

                if (criteria.NotUserId.HasValue)
                {
                    query = query.Where(x => !x.Users.Any(u => u.Id == criteria.NotUserId.Value));
                }
                if (!string.IsNullOrEmpty(criteria.CsvSynonyms))
                {
                    query = query.Where(x => x.CsvSynonyms.Contains(criteria.CsvSynonyms));
                }

            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<LoginSite> Order(IQueryable<LoginSite> query, SearchBase<LoginSite> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "siteName":
                        query = descending ? query.OrderByDescending(x => x.SiteName) : query.OrderBy(x => x.SiteName);
                        break;
                    case "csvSynonyms":
                        query = descending ? query.OrderByDescending(x => x.CsvSynonyms) : query.OrderBy(x => x.CsvSynonyms);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
