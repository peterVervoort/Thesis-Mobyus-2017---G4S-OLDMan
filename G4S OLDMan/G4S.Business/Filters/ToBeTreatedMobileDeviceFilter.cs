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
    internal class ToBeTreatedMobileDeviceFilter : EntityFilterBase<ToBeTreatedMobileDevice>, IEntityFilter<ToBeTreatedMobileDevice>
    {
        [Dependency]
        public ISecurityService SecurityService { get; set; }

        public override async Task<IQueryable<ToBeTreatedMobileDevice>> FilterAsync(IQueryable<ToBeTreatedMobileDevice> query, SearchBase<ToBeTreatedMobileDevice> searchCriteria)
        {
            //Data filtering on user sites
            var user = await SecurityService.GetCurrentUser();
            if (user != null)
            {
                var loginSiteIds = user.LoginSites.Select(l => l.Id).ToList();
                query = query.Where(md => md.LoginSiteId.HasValue && loginSiteIds.Contains(md.LoginSiteId.Value));
            }


            if (searchCriteria.GetType().Equals(typeof(ToBeTreatedMobileDeviceSearchCriteria)))
            {

                ToBeTreatedMobileDeviceSearchCriteria criteria = (ToBeTreatedMobileDeviceSearchCriteria)searchCriteria;

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
                if (!string.IsNullOrWhiteSpace(criteria.Type))
                {
                    query = query.Where(x => x.Type.TypeName.Contains(criteria.Type));
                }

                if(!string.IsNullOrWhiteSpace(criteria.LoginSite))
                {
                    query = query.Where(x => x.LoginSite.SiteName.Contains(criteria.LoginSite));
                }
            }
            return await base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<ToBeTreatedMobileDevice> Order(IQueryable<ToBeTreatedMobileDevice> query, SearchBase<ToBeTreatedMobileDevice> searchCriteria)
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