using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class TranslationFilter : EntityFilterBase<Translation>, IEntityFilter<Translation>
    {
        public override Task<IQueryable<Translation>> FilterAsync(IQueryable<Translation> query, SearchBase<Translation> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(TranslationSearchCriteria))) {

                TranslationSearchCriteria criteria = (TranslationSearchCriteria)searchCriteria;

                if (!string.IsNullOrWhiteSpace(criteria.TaalShortCode))
                {
                    query = query.Where(x => x.Language.ShortCode.Contains(criteria.TaalShortCode));
                }

                if (!string.IsNullOrEmpty(criteria.Language))
                {
                    query = query.Where(x => x.Language.Taal.Contains(criteria.Language));
                }

                if (!string.IsNullOrEmpty(criteria.Group))
                {
                    query = query.Where(x => x.Group.Contains(criteria.Group));
                }

                if (!string.IsNullOrEmpty(criteria.Keyword))
                {
                    query = query.Where(x => x.Keyword.Contains(criteria.Keyword));
                }

                if (!string.IsNullOrEmpty(criteria.Value))
                {
                    query = query.Where(x => x.Value.Contains(criteria.Value));
                }
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<Translation> Order(IQueryable<Translation> query, SearchBase<Translation> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "language":
                        query = descending ? query.OrderByDescending(x => x.Language.Taal) : query.OrderBy(x => x.Language.Taal);
                        break;
                    case "group":
                        query = descending ? query.OrderByDescending(x => x.Group) : query.OrderBy(x => x.Group);
                        break;
                    case "keyword":
                        query = descending ? query.OrderByDescending(x => x.Keyword) : query.OrderBy(x => x.Keyword);
                        break;
                    case "value":
                        query = descending ? query.OrderByDescending(x => x.Value) : query.OrderBy(x => x.Value);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
