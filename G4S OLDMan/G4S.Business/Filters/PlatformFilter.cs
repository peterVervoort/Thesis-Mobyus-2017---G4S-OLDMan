
using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class PlatformFilter : EntityFilterBase<Platform>, IEntityFilter<Platform>
    {
        public override Task<IQueryable<Platform>> FilterAsync(IQueryable<Platform> query, SearchBase<Platform> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(PlatformSearchCriteria)))
            {

                PlatformSearchCriteria criteria = (PlatformSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.PlatformName))
                {
                    query = query.Where(x => x.PlatformName.Contains(criteria.PlatformName));
                }

                if (!string.IsNullOrEmpty(criteria.CsvSynonyms))
                {
                    query = query.Where(x => x.CsvSynonyms.Contains(criteria.CsvSynonyms));
                }

            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<Platform> Order(IQueryable<Platform> query, SearchBase<Platform> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "platform":
                        query = descending ? query.OrderByDescending(x => x.PlatformName) : query.OrderBy(x => x.PlatformName);
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
