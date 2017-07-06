using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class LanguageFilter : EntityFilterBase<Language>, IEntityFilter<Language>
    {
        public override Task<IQueryable<Language>> FilterAsync(IQueryable<Language> query, SearchBase<Language> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(LanguageSearchCriteria))) {

                LanguageSearchCriteria criteria = (LanguageSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.Taal))
                {
                    query = query.Where(x => x.Taal.Contains(criteria.Taal));
                }

                if (!string.IsNullOrEmpty(criteria.ShortCode))
                {
                    query = query.Where(x => x.ShortCode.Contains(criteria.ShortCode));
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<Language> Order(IQueryable<Language> query, SearchBase<Language> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "taal":
                        query = descending ? query.OrderByDescending(x => x.Taal) : query.OrderBy(x => x.Taal);
                        break;
                    case "shortCode":
                        query = descending ? query.OrderByDescending(x => x.ShortCode) : query.OrderBy(x => x.ShortCode);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
