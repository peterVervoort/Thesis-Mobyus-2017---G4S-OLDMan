using G4S.Business.Services;
using G4S.DataAccess.Repositories;
using G4S.Entities.Enums;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class MobileDeviceFilter : EntityFilterBase<MobileDevice>, IEntityFilter<MobileDevice>
    {
        [Dependency]
        public ISecurityService SecurityService { get; set; }

        public override async Task<IQueryable<MobileDevice>> FilterAsync(IQueryable<MobileDevice> query, SearchBase<MobileDevice> searchCriteria)
        {
            //Data filtering on user sites
            var user = await SecurityService.GetCurrentUser();
            if (user != null)
            {
                var loginSiteIds = user.LoginSites.Select(l => l.Id).ToList();
                query = query.Where(md => md.LoginSiteId.HasValue && loginSiteIds.Contains(md.LoginSiteId.Value));
            }


            if (searchCriteria.GetType().Equals(typeof(MobileDeviceSearchCriteria)))
            {

                MobileDeviceSearchCriteria criteria = (MobileDeviceSearchCriteria)searchCriteria;

                if (criteria.OrderItemId.HasValue)
                {
                    query = query.Where(x => x.OrderItemId == criteria.OrderItemId.Value);
                }
                if (criteria.NotOrderItemId.HasValue)
                {
                    query = query.Where(x => x.OrderItemId != criteria.NotOrderItemId.Value);
                }
                if (criteria.DeviceTypeId.HasValue)
                {
                    query = query.Where(x => x.DeviceTypeId == criteria.DeviceTypeId.Value);
                }
                if (!string.IsNullOrWhiteSpace(criteria.DeviceName))
                {
                    query = query.Where(x => x.DeviceName.Contains(criteria.DeviceName));
                }
                if (!string.IsNullOrEmpty(criteria.Reference))
                {
                    query = query.Where(x => x.Reference.Contains(criteria.Reference));
                }
                if (!string.IsNullOrEmpty(criteria.TagName))
                {
                    query = query.Where(x => x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault() != null && 
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange != null &&
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange.StateTo != null && 
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange.StateTo.Tag == criteria.TagName);
                }
                if (!string.IsNullOrWhiteSpace(criteria.TypeName))
                {
                    query = query.Where(x => x.Type.TypeName.Contains(criteria.TypeName));
                }

                if(!string.IsNullOrWhiteSpace(criteria.LoginSite))
                {
                    query = query.Where(x => x.LoginSite.SiteName.Contains(criteria.LoginSite));
                }

                if (!string.IsNullOrWhiteSpace(criteria.StateName))
                {
                    query = query.Where(x => x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault() != null &&
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange != null &&
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange.StateTo != null &&
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange.StateTo.Name.Contains(criteria.StateName));
                }
                if (criteria.SpareState.HasValue)
                {
                    query = query.Where(x => x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault() != null &&
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange != null &&
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange.StateTo != null &&
                                             x.RepairChanges.OrderByDescending(rc => rc.ChangeDate).FirstOrDefault().RepairStateChange.StateTo.IsSpare == criteria.SpareState.Value);
                }
            }
            return await base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<MobileDevice> Order(IQueryable<MobileDevice> query, SearchBase<MobileDevice> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "type":
                        query = descending ? query.OrderByDescending(x => x.Type.TypeName) : query.OrderBy(x => x.Type.TypeName);
                        break;
                    case "reference":
                        query = descending ? query.OrderByDescending(x => x.Reference) : query.OrderBy(x => x.Reference);
                        break;
                    case "deviceName":
                        query = descending ? query.OrderByDescending(x => x.DeviceName) : query.OrderBy(x => x.DeviceName);
                        break;
                    case "currentState":
                        query = descending ? query.OrderByDescending(x => x.LastStateHistory != null ? x.LastStateHistory.RepairStateChange.StateTo.Name : "-") : query.OrderBy(x => x.LastStateHistory != null ? x.LastStateHistory.RepairStateChange.StateTo.Name : "-");
                        break;
                    case "lastStateDate":
                        query = descending ? query.OrderByDescending(x => x.LastStateHistory.ChangeDate) : query.OrderBy(x => x.LastStateHistory.ChangeDate);
                        break;
                    case "loginSite":
                        query = descending ? query.OrderByDescending(x => x.LoginSite.SiteName) : query.OrderBy(x => x.LoginSite.SiteName);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}