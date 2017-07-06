using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class DeviceStateHistoryFilter : EntityFilterBase<DeviceStateHistory>, IEntityFilter<DeviceStateHistory>
    {
        public override Task<IQueryable<DeviceStateHistory>> FilterAsync(IQueryable<DeviceStateHistory> query, SearchBase<DeviceStateHistory> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(DeviceStateHistorySearchCriteria)))
            {

                DeviceStateHistorySearchCriteria criteria = (DeviceStateHistorySearchCriteria)searchCriteria;

                if (criteria.MobileDeviceId.HasValue)
                {
                    query = query.Where(x => x.MobileDeviceId == criteria.MobileDeviceId.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.Comment))
                {
                    query = query.Where(x => x.Comment.Contains(criteria.Comment));
                }

                if (criteria.RepairStateChangeId.HasValue)
                {
                    query = query.Where(x => x.RepairStateChangeId == criteria.RepairStateChangeId.Value);
                }

                if (criteria.StateFromId.HasValue)
                {
                    query = query.Where(x => x.RepairStateChange.StateFromId == criteria.StateFromId.Value);
                }

                if (criteria.StateToId.HasValue)
                {
                    query = query.Where(x => x.RepairStateChange.StateToId == criteria.StateToId.Value);
                }
                
            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<DeviceStateHistory> Order(IQueryable<DeviceStateHistory> query, SearchBase<DeviceStateHistory> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "comment":
                        query = descending ? query.OrderByDescending(x => x.Comment) : query.OrderBy(x => x.Comment);
                        break;
                    case "changeDate":
                        query = descending ? query.OrderByDescending(x => x.ChangeDate) : query.OrderBy(x => x.ChangeDate);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
