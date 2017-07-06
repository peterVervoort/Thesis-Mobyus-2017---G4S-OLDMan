using System.Linq;
using G4S.DataAccess.Repositories;
using G4S.Entities.Pocos;
using G4S.Entities.SearchPocos;
using System.Threading.Tasks;

namespace G4S.Business.Filters
{
    internal class ProductTypeFilter : EntityFilterBase<ProductType>, IEntityFilter<ProductType>
    {
        public override Task<IQueryable<ProductType>> FilterAsync(IQueryable<ProductType> query, SearchBase<ProductType> searchCriteria)
        {
            if (searchCriteria.GetType().Equals(typeof(ProductTypeSearchCriteria)))
            {

                ProductTypeSearchCriteria criteria = (ProductTypeSearchCriteria)searchCriteria;


                if (!string.IsNullOrWhiteSpace(criteria.TypeName))
                {
                    query = query.Where(x => x.TypeName.Contains(criteria.TypeName));
                }

                if (criteria.DeviceTypeRequired.HasValue)
                {
                    query = query.Where(x => x.DeviceTypeRequired == criteria.DeviceTypeRequired.Value);
                }

                if (criteria.HasOrderStates.HasValue)
                {
                    query = query.Where(x => x.HasOrderStates == criteria.HasOrderStates.Value);
                }

                if (criteria.LoginLicenceRequired.HasValue)
                {
                    query = query.Where(x => x.LoginLicenceRequired == criteria.LoginLicenceRequired.Value);
                }
                

            }

            return base.FilterAsync(query, searchCriteria);
        }

        public override IQueryable<ProductType> Order(IQueryable<ProductType> query, SearchBase<ProductType> searchCriteria)
        {
            if (!string.IsNullOrEmpty(searchCriteria.SortField))
            {
                var descending = searchCriteria.SortDescending.HasValue && searchCriteria.SortDescending.Value;
                switch (searchCriteria.SortField)
                {
                    case "typeName":
                        query = descending ? query.OrderByDescending(x => x.TypeName) : query.OrderBy(x => x.TypeName);
                        break;
                    case "deviceTypeRequired":
                        query = descending ? query.OrderByDescending(x => x.DeviceTypeRequired) : query.OrderBy(x => x.DeviceTypeRequired);
                        break;
                    case "hasOrderStates":
                        query = descending ? query.OrderByDescending(x => x.HasOrderStates) : query.OrderBy(x => x.HasOrderStates);
                        break;
                    case "loginLicenceRequired":
                        query = descending ? query.OrderByDescending(x => x.LoginLicenceRequired) : query.OrderBy(x => x.LoginLicenceRequired);
                        break;
                    default:
                        break;
                }
            }

            return base.Order(query, searchCriteria);
        }
    }
}
