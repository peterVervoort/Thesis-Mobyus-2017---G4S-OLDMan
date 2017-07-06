using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class DeviceTypeFilter : EntityFilterBase<DeviceType>, IEntityFilter<DeviceType>
    {
        public override Task<IQueryable<DeviceType>> FilterAsync(IQueryable<DeviceType> query, SearchBase<DeviceType> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(DeviceTypeSearchCriteria)))
            {

                DeviceTypeSearchCriteria criteria = (DeviceTypeSearchCriteria)searchCriteria;


                if (!string.IsNullOrWhiteSpace(criteria.TypeName))
                {
                    query = query.Where(x => x.TypeName.Contains(criteria.TypeName));
                }

                if (criteria.LwpSettingPossible.HasValue)
                {
                    query = query.Where(x => x.LwpSettingPossible == criteria.LwpSettingPossible.Value);
                }

                if (!string.IsNullOrEmpty(criteria.CsvSynonyms))
                {
                    query = query.Where(x => x.CsvSynonyms.Contains(criteria.CsvSynonyms));
                }

            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<DeviceType> Order(IQueryable<DeviceType> query, SearchBase<DeviceType> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "typeName":
                        query = descending ? query.OrderByDescending(x => x.TypeName) : query.OrderBy(x => x.TypeName);
                        break;
                    case "lwpSettingPossible":
                        query = descending ? query.OrderByDescending(x => x.LwpSettingPossible) : query.OrderBy(x => x.LwpSettingPossible);
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
