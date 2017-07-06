using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class LoginLicenceFilter : EntityFilterBase<LoginLicence>, IEntityFilter<LoginLicence>
    {
        public override Task<IQueryable<LoginLicence>> FilterAsync(IQueryable<LoginLicence> query, SearchBase<LoginLicence> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(LoginLicenceSearchCriteria)))
            {

                LoginLicenceSearchCriteria criteria = (LoginLicenceSearchCriteria)searchCriteria;

                if (criteria.CertificateCreated.HasValue)
                {
                    query = query.Where(x => x.CertificateCreated == criteria.CertificateCreated.Value);
                }

                if (criteria.OrderItemId.HasValue)
                {
                    query = query.Where(x => x.OrderItemId == criteria.OrderItemId.Value);
                }

                if (criteria.NotOrderItemId.HasValue)
                {
                    query = query.Where(x => x.OrderItemId != criteria.NotOrderItemId.Value);
                }

                if (criteria.PlatformId.HasValue)
                {
                    query = query.Where(x => x.PlatformId == criteria.PlatformId.Value);
                }

                if (criteria.LoginSiteId.HasValue)
                {
                    query = query.Where(x => x.LoginSiteId == criteria.LoginSiteId.Value);
                }

                if (!string.IsNullOrEmpty(criteria.Platform))
                {
                    query = query.Where(x => x.Platform.PlatformName.Contains(criteria.Platform));
                }

                if (!string.IsNullOrEmpty(criteria.LoginSite))
                {
                    query = query.Where(x => x.LoginSite.SiteName.Contains(criteria.LoginSite));
                }

                if (!string.IsNullOrEmpty(criteria.PurchaseOrderNumber))
                {
                    query = query.Where(x => x.OrderItem.PurchaseOrder.PurchaseOrderNumber.ToString().Contains(criteria.PurchaseOrderNumber));
                }

            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<LoginLicence> Order(IQueryable<LoginLicence> query, SearchBase<LoginLicence> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "certificateCreated":
                        query = descending ? query.OrderByDescending(x => x.CertificateCreated) : query.OrderBy(x => x.CertificateCreated);
                        break;
                    case "orderItemId":
                        query = descending ? query.OrderByDescending(x => x.OrderItemId) : query.OrderBy(x => x.OrderItemId);
                        break;
                    case "loginSiteId":
                        query = descending ? query.OrderByDescending(x => x.LoginSiteId) : query.OrderBy(x => x.LoginSiteId);
                        break;
                    case "platformId":
                        query = descending ? query.OrderByDescending(x => x.PlatformId) : query.OrderBy(x => x.PlatformId);
                        break;
                    case "loginSite":
                        query = descending ? query.OrderByDescending(x => x.LoginSite.SiteName) : query.OrderBy(x => x.LoginSite.SiteName);
                        break;
                    case "platform":
                        query = descending ? query.OrderByDescending(x => x.Platform.PlatformName) : query.OrderBy(x => x.Platform.PlatformName);
                        break;
                    case "purchaseOrderNumber":
                        query = descending ? query.OrderByDescending(x => x.OrderItem.PurchaseOrder.PurchaseOrderNumber) : query.OrderBy(x => x.OrderItem.PurchaseOrder.PurchaseOrderNumber);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
